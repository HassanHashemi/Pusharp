using System;
using System.Threading.Tasks;

namespace MessageBus.Client
{
    public interface IMessageBusClient
    {
        event EventHandler<MessageBusEventArgs> MessageReceived;
        Task Start();
        bool Connected { get; }
    }
}
