using System;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace MessageBus.Core
{

    public interface IMessageBusServer
    {
        Task PostMessage(IMessageBusMessage message);
        Task AddClient(WebSocket socket);
    }
}