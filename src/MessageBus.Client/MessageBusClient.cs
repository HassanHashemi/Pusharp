using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageBus.Client
{
    public class WebSocketMessageBusClient : IMessageBusClient
    {
        private const int BUFFER_SIZE = 1024;
        private ClientWebSocket _socket;

        public event EventHandler<MessageBusEventArgs> MessageReceived;

        public bool Connected { get; set; }

        private ClientWebSocket Socket
        {
            get
            {
                return _socket;
            }
            set
            {
                _socket = value;
            }
        }

        public WebSocketMessageBusClient(Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException("uri");
            }
            if (uri.Scheme != "ws")
            {
                throw new InvalidOperationException("only ws scheme is accepted");
            }

            this.Uri = uri;
        }

        public Uri Uri { get; private set; }
        public async Task Start(CancellationToken token)
        {
            this.Socket = new ClientWebSocket();
            await Socket.ConnectAsync(this.Uri, token).ConfigureAwait(false);
            this.Connected = true;
            await this.Receive();
        }


        public async Task Receive()
        {
            var buffer = WebSocket.CreateClientBuffer(BUFFER_SIZE, BUFFER_SIZE);

            while (Socket.State == WebSocketState.Open)
            {
                string message = await ReceiveText(this.Socket);

                if (this.MessageReceived != null)
                {
                    this.MessageReceived.Invoke(this, new MessageBusEventArgs() { Message = message });
                }
            }
            this.Connected = false;
        }

        public Task Start()
        {
            return this.Start(CancellationToken.None);
        }

        private async Task<string> ReceiveText(ClientWebSocket socket)
        {
            var buffer = WebSocket.CreateClientBuffer(BUFFER_SIZE, BUFFER_SIZE);
            var result = await socket.ReceiveAsync(buffer, CancellationToken.None);

            if (result.MessageType != WebSocketMessageType.Text)
            {
                throw new InvalidOperationException("message must be text");
            }
            return await this.ReceiveTextInternal(socket, result, buffer);
        }

        private async Task<string> ReceiveTextInternal(ClientWebSocket Socket, WebSocketReceiveResult result, ArraySegment<byte> buffer)
        {
            string message = string.Empty;

            if (buffer.Array?.Length == 0)
            {
                // read the current data inside buffer
                message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            }

            while (!(result = await Socket.ReceiveAsync(buffer, CancellationToken.None)).EndOfMessage)
                message += Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

            return message;
        }
    }
}