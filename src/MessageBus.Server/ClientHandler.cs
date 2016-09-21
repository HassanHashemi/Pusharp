using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace MessageBus.Server
{
    public class ClientHandler : HttpTaskAsyncHandler
    {
        public override Task ProcessRequestAsync(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessSocket);
            }
            else
            {
                context.Response.Write("only websocket is supported");
                context.Response.Flush();
                context.Response.End();
            }
            return Task.FromResult(0);
        }

        private Task ProcessSocket(AspNetWebSocketContext arg)
        {
            var socket = arg.WebSocket;
            return ProcessSocketInternal(socket);
        }

        private async Task ProcessSocketInternal(WebSocket socket)
        {
            Core.IMessageBusServer server = Core.WebSocketMessageBusServer.Current;
            await server.AddClient(socket);
        }
    }
}