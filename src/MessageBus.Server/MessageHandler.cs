using MessageBus.Core;
using System.Threading.Tasks;
using System.Web;

namespace MessageBus.Server
{

    // TODO: Fix the mess you have made with DI
    // this class wil receive instructions from the web service
    public class MessageHandler : HttpTaskAsyncHandler
    {
        public override async Task ProcessRequestAsync(HttpContext context)
        {
            var json = context.Request.Form["json"];
            await WebSocketMessageBusServer.Current.PostMessage(new JSONMessage(json));
        }
    }

}