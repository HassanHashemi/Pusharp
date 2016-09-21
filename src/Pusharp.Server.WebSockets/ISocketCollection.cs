using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Pusharp.Server.WebSockets
{
    public interface ISocketCollection<TID> : IEnumerable<KeyValuePair<TID, Tuple<WebSocket, ClientInfo>>>
    {
        bool TryAdd(TID id, WebSocket socket, ClientInfo info);
        bool TryGetClientInfo(TID id, out ClientInfo info);
        bool TryGetSocket(TID  id, out WebSocket socket);
        bool TryRemove(TID id);
        void SendToAll(TID message);
        void SendToClient(TID id, string message);
    }
}