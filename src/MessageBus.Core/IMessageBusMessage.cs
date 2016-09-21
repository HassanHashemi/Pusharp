
namespace MessageBus.Core
{
    public interface IMessageBusMessage
    {
        string Data { get; set; }
    }
}