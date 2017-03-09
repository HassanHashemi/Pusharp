using System;

namespace MessageBus.Client
{
    public class MessageBusEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
