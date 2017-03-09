using MessageBus.Client;
using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Pusharp.Server.WebSockets
{
    public class DeviceSocketCollection : ConcurrentDictionary<string, Tuple<WebSocket, ClientInfo>>, ISocketCollection<string>
    {
        private static DeviceSocketCollection _current;

        //private readonly IMessageBusClient _bus = new WebSocketMessageBusClient(null/* pass in the bus address*/);

        public static DeviceSocketCollection Current
        {
            get
            {
                return _current ?? (_current = new DeviceSocketCollection());
            }
        }

        private DeviceSocketCollection () 
        {
            
        }

        //private MessageBroker _broker = new MessageBroker();

        public bool TryAdd(string id, WebSocket socket, ClientInfo info)
        {
            return base.TryAdd(id, new Tuple<WebSocket, ClientInfo>(socket, info));
        }

        public bool TryGetClientInfo(string id, out ClientInfo info)
        {
            Tuple<WebSocket, ClientInfo> t;
            bool result = TryGetValue(id, out t);
            info = t.Item2;
            return result;
        }

        public bool TryGetSocket(string id, out WebSocket socket)
        {
            Tuple<WebSocket, ClientInfo> t;
            bool result = TryGetValue(id, out t);
            socket = t.Item1;
            return result;
        }
        
        public void SendToAll(string message)
        {
            foreach (var item in this)
            {
                //_broker.Post(item.Value.Item1, message);
            }
        }

        public void SendToClient(string id, string message)
        {
            WebSocket socket;
            this.TryGetSocket(id, out socket);
           // _broker.Post(socket, message);
        }

        public bool TryRemove(string id)
        {
            Tuple<WebSocket, ClientInfo> ignore;

            return base.TryRemove(id, out ignore);
        }

        public void Receive(object sender, MessageBusEventArgs e)
        {
            this.SendToAll(e.Message);
        }
    }
}