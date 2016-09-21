using System;

namespace Pusharp.Server.WebSockets
{
    public class TextMessageEventArgs: EventArgs
    {
        public string Message { get; set; }
    }
}