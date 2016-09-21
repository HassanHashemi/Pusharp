using Pusharp.Server.WebSockets.AppEvents;

namespace ZSoft.PushServices.WebSockets.AppEvents.Handlers
{
    public class MessageBusClientBootstrap : IAppEvent
    {
        public AppEventType EvenType
        {
            get
            {
                return AppEventType.App_Start;
            }
        }

        public void Execute_AppHandler()
        {
            // TODO: initialize the message bus client for current server node..
        }
    }
}