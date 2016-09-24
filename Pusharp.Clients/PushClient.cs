using Pusharp.Net.DTO;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pusharp.Clients
{
    public class PushClient
    {
        private const int BUFFER_SIZE = 4 * 1024;
        private const int HEARTBEAT_INTERVAL = 11000;
        private Task _heartBeatWorker;
        private readonly byte[] HEARTBEAT_DATA = new byte[] { 0 };


        public PushClient(Uri uri)
        {
            this.Uri = uri;
        }

        public ClientWebSocket Socket { get; set; }

        public Uri Uri { get; set; }

        public event EventHandler<NotificationReceivedEventArgs> NotificationReceived;
        public event EventHandler Disconnected;
        public event EventHandler Connected;

        public async Task Stop()
        {
            if (this.Socket.State == WebSocketState.Open)
            {
                await this.Socket.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None).
                    ContinueWith(t =>
                    {
                        this.OnDissConnected(new EventArgs());
                    }).ConfigureAwait(false);
            }

        }

        protected void OnConnected(EventArgs e)
        {
            if (this.Connected != null)
            {
                this.Connected(this, e);
            }
        }

        protected void OnDissConnected(EventArgs e)
        {
            if (this.Disconnected != null)
            {
                this.Disconnected(this, e);
            }
        }

        public Task Start(CancellationToken token)
        {
            try
            {
                var connectTask = this.Socket.ConnectAsync(this.Uri, token);
                connectTask.ContinueWith(t =>
                {
                    this.OnConnected(new EventArgs());
                    this.OnDissConnected(new EventArgs());
                    this.ProcessSocket();
                    this.StartHeartbeat();
                }, TaskContinuationOptions.OnlyOnRanToCompletion);

                return connectTask;
            }
            catch (Exception ex)
            {
                TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
                tcs.SetException(ex);

                return tcs.Task;
            }
        }


        private void StartHeartbeat()
        {
            CancellationTokenSource periodicTaskTokenSource = new CancellationTokenSource();
            this._heartBeatWorker = Task.Factory.StartPeriodic(() =>
            {
                try
                {
                    if (this.Socket.State == WebSocketState.Open)
                    {
                        Socket.SendAsync(
                            new ArraySegment<byte>(this.HEARTBEAT_DATA),
                            WebSocketMessageType.Binary,
                            true,
                            CancellationToken.None);
                    }
                }
                catch (Exception)
                {

                }
            }, HEARTBEAT_INTERVAL, periodicTaskTokenSource.Token);
            this.Disconnected += (s, ea) => periodicTaskTokenSource.Cancel();

        }

        protected void OnMessageReceived(NotificationReceivedEventArgs args)
        {
            if (this.NotificationReceived != null)
            {
                this.NotificationReceived(this, args);
            }
        }


        private async void ProcessSocket()
        {
            var currentMessage = string.Empty;
            var buffer = WebSocket.CreateClientBuffer(BUFFER_SIZE, BUFFER_SIZE);
            while (this.Socket.State == WebSocketState.Open)
            {
                var result = await Socket.ReceiveAsync(buffer, CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    currentMessage += Encoding.UTF8.GetString(buffer.Array, 9, result.Count);
                    while (!result.EndOfMessage)
                    {
                        result = await Socket.ReceiveAsync(buffer, CancellationToken.None);
                        currentMessage += Encoding.UTF8.GetString(buffer.Array, 9, result.Count);
                    }

                    this.OnMessageReceived(new NotificationReceivedEventArgs()
                    {
                        Notification = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationDTO>(String.Copy(currentMessage))
                    });
                    currentMessage = string.Empty;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
