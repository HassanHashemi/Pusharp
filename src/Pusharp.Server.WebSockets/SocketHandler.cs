using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;

namespace Pusharp.Server.WebSockets
{
    
    public class SocketHandler : HttpTaskAsyncHandler
    {
        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
        public override Task ProcessRequestAsync(HttpContext context)
        {
            if (context.IsWebSocketRequest)
            {
                context.AcceptWebSocketRequest(ProcessSocket);
            }
            else
            {
                context.Response.StatusCode = 404;
                
            }

            return Task.FromResult(0);
        }

        private async Task ProcessSocket(AspNetWebSocketContext arg)
        {
            await Server.Current.Add(arg.WebSocket);
        }
    }
}