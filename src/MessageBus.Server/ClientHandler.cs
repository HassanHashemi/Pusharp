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

        private async Task ProcessSocket(AspNetWebSocketContext arg)
        {
            var socket = arg.WebSocket;
            await Core.WebSocketMessageBusServer.Current.AddClient(socket);
        }
    }
}