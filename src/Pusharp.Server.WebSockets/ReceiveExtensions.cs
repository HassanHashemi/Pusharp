using System.Net.WebSockets;

namespace Pusharp.Server.WebSockets
{
    public static class ReceiveExtensions
    {
        private const int HEARTBEAT_LENGTH = 1;

        public static bool IsHeartbeat(this WebSocketReceiveResult source)
        {
            return source.Count == HEARTBEAT_LENGTH  && source.MessageType == WebSocketMessageType.Binary;
        }
    }
}