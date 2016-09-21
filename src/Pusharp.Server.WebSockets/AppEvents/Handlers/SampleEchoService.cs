
using Pusharp.Server.WebSockets;

namespace Pusharp.Server.WebSockets.AppEvents.Handlers
{
    public class SampleEchoService : IAppEvent
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
            Server.Current.TextMessageReceived += Server_TextMessageReceived;
        }

        private void Server_TextMessageReceived(object sender, MessageEventArgs<string> e)
        {
            DeviceSocketCollection.Current.SendToClient(e.SocketId, e.Message);
        }
    }
}