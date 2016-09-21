using System;

namespace MessageBus.Core
{
    public class MessageBusServerEventArgs : EventArgs
    {
        public IMessageBusMessage Message { get; set; }
    }

}