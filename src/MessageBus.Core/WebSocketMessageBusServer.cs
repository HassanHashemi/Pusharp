using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBus.Core
{
    public sealed class WebSocketMessageBusServer : IMessageBusServer
    {

        private ConcurrentDictionary<int, WebSocket> _clients = new ConcurrentDictionary<int, WebSocket>();
        public ConcurrentDictionary<int, WebSocket> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
            }
        }


        private static IMessageBusServer _current;
        public static IMessageBusServer Current
        {
            get
            {
                // TODO: injection should be done by using some DI technique
                return _current ?? (_current = new WebSocketMessageBusServer());
            }
        }

        public async Task PostMessage(IMessageBusMessage message)
        {
            string jsonMessage = message.ToJson();
            foreach (var kvp in this.Clients)
            {
                await Send(kvp.Value, jsonMessage);
            }
        }

        public async Task AddClient(WebSocket socket)
        {
            if (this.Clients.TryAdd(socket.GetHashCode(), socket))
            {
                while (socket.State == WebSocketState.Open)
                {
                    try
                    {
                        // we will never receive data from clients, however we must await the socket in order to receive hearbeat and also
                        // to prevent client from being recycled by iis native runtime engine
                        await socket.ReceiveAsync(WebSocket.CreateClientBuffer(128, 128), CancellationToken.None);
                    }
                    catch (Exception)
                    {

                    }
                }

                // client is dead
                // remove client from client collection
                this.Clients.TryRemove(socket.GetHashCode(), out socket);
            }
        }

        private Task Send(WebSocket socket, string jsonMessage)
        {
            try
            {
                return socket.SendAsync(new ArraySegment<byte>(Encoding.UTF8.GetBytes(jsonMessage)),
                    WebSocketMessageType.Text,
                    true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                // IGNORE THE exception
                TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                tcs.SetException(ex);
                return tcs.Task;
            }
        }
    }

}
