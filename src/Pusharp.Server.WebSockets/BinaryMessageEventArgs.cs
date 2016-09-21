using System;
using System.Net.WebSockets;

namespace Pusharp.Server.WebSockets
{
    public class MessageEventArgs<T> where T : class
    {
        public T Message { get; set; }
        public WebSocket Socket { get; set; }
        public string SocketId { get; set; }
    }
}