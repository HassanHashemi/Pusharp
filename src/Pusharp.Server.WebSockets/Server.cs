using MessageBus.Client;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pusharp.Server.WebSockets
{
    public class Server
    {
        private const int IDENTIFICATION_TIMEOUT = 10000;
        private const int HEARTBEAT_TIMEOUT = 25000;
        // TODO: 

        private static Server _current;
        private readonly ISocketCollection<string> _sockets = DeviceSocketCollection.Current;

        public Server()
        {

        }

        public ISocketCollection<string> Clients
        {
            get
            {
                return _sockets;
            }
        }

        public static Server Current
        {
            get
            {
                return _current ?? (_current = new Server());
            }
        }

        public event EventHandler<MessageEventArgs<string>> TextMessageReceived;
        public event EventHandler<MessageEventArgs<byte[]>> BinaryMessageReceived;

        internal async Task Add(WebSocket socket)
        {
            ClientInfo info = null;
            if ((info = await TryGetInfo(socket)) == null)
            {
                return;
            }

            if (!_sockets.TryAdd(info.DeviceId, socket, info))
            {
                return;
            }

            await ProcessSocket(info.DeviceId, socket);
            _sockets.TryRemove(info.DeviceId);
        }

        private async Task<ClientInfo> TryGetInfo(WebSocket socket)
        {
            ArraySegment<byte> buffer = WebSocket.CreateClientBuffer(1024, 1024);
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(IDENTIFICATION_TIMEOUT);
            var recieveTask = this.ReceiveSocket(socket, buffer, cts.Token, retry: 0);

            ClientInfo clientInfo = null;
            await recieveTask.ContinueWith(async t =>
                {
                    var result = t.Result;

                    if (result.MessageType != WebSocketMessageType.Text)
                    {
                        return;
                    }

                    string jsonInfo = await ReceiveText(socket, result, buffer);

                    if (!ClientInfo.TryParseJson(jsonInfo, out clientInfo))
                    {
                        socket.ShutDown().Forget();
                        return;
                    }

                }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return clientInfo;
        }

        private async Task ProcessSocket(string id, WebSocket socket)
        {
            var buffer = WebSocket.CreateClientBuffer(1024, 1024);

            while (socket.State == WebSocketState.Open)
            {
                using (CancellationTokenSource cts = new CancellationTokenSource())
                {
                    cts.CancelAfter(HEARTBEAT_TIMEOUT);

                    await ReceiveSocket(socket, buffer, cts.Token)
                        .ContinueWith(async t =>
                        {
                            var result = t.Result;

                            switch (result.MessageType)
                            {
                                case WebSocketMessageType.Text:
                                    await ReceiveText(id, socket, buffer, result);
                                    break;
                                case WebSocketMessageType.Close:
                                    socket.ShutDown().Forget();
                                    break;
                                case WebSocketMessageType.Binary:
                                    if (!t.Result.IsHeartbeat())
                                    {
                                        throw new NotSupportedException("Biary Messages are not supported at this time...");
                                    }
                                    break;
                            }
                        }, TaskContinuationOptions.OnlyOnRanToCompletion);
                }
            }
        }

        private Task ReceiveText(string id, WebSocket socket, ArraySegment<byte> buffer, WebSocketReceiveResult result)
        {
            return 
                ReceiveText(socket, result, buffer)
                    .ContinueWith(t2 =>
                    {
                        OnTextMessageReceived(new MessageEventArgs<string>()
                        {
                            Socket = socket,
                            Message = t2.Result,
                            SocketId = id
                        });
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        protected void OnTextMessageReceived(MessageEventArgs<string> messageEventArgs)
        {
            if (this.TextMessageReceived != null)
            {
                this.TextMessageReceived.Invoke(this, messageEventArgs);
            }
        }

        private async Task<byte[]> ReceiveBinaryData(WebSocket socket, WebSocketReceiveResult result, ArraySegment<byte> buffer)
        {
            // not supported yet.
            await Task.Delay(1);
            return new byte[1];
        }

        private Task<WebSocketReceiveResult> ReceiveSocket(WebSocket socket, ArraySegment<byte> buffer, CancellationToken token, int retry = 3)
        {
            if (retry < 0)
            {
                throw new ArgumentException("retry can not be less than 0");
            }
            try
            {
                return socket.ReceiveAsync(buffer, token);
            }
            catch (InvalidOperationException ex)
            {
                if (retry > 0)
                {
                    return ReceiveSocket(socket, buffer, token);
                }
                else
                {
                    return Task.Factory.CreateFaultedTask<WebSocketReceiveResult>(ex);
                }
            }
            catch (Exception ex)
            {
                return Task.Factory.CreateFaultedTask<WebSocketReceiveResult>(ex);
            }
        }


        private Task<WebSocketReceiveResult> ReceiveSocket(WebSocket socket, ArraySegment<byte> buffer)
        {
            return ReceiveSocket(socket, buffer, CancellationToken.None);
        }

        private async Task<string> ReceiveText(WebSocket socket, WebSocketReceiveResult receiveResult, ArraySegment<byte> buffer)
        {
            string result = DecodeTextBuffer(buffer, 0, receiveResult.Count);

            while (!receiveResult.EndOfMessage)
            {
                var receiveTask = ReceiveSocket(socket, buffer);
                await receiveTask.ContinueWith(t =>
                    {
                        receiveResult = t.Result;
                        result += DecodeTextBuffer(buffer, 0, receiveResult.Count);
                    }, TaskContinuationOptions.OnlyOnRanToCompletion);
            }

            return result;
        }

        private string DecodeTextBuffer(ArraySegment<byte> buffer, int offSet, int count)
        {
            return Encoding.UTF8.GetString(buffer.Array, offSet, count);
        }
    }
}