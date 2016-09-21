
namespace Pusharp.Server.WebSockets.AppEvents
{
    internal interface IAppEvent
    {
        AppEventType EvenType { get; }
        void Execute_AppHandler();
    }

}