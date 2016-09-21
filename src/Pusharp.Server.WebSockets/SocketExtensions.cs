using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Pusharp.Server.WebSockets
{
    public static class SocketExtensions
    {
        public static Task ShutDown(this WebSocket source)
        {
            try
            {
                return source.CloseAsync(WebSocketCloseStatus.Empty, string.Empty, CancellationToken.None);
            }
            catch(Exception ex)
            {
                return Task.Factory.CreateFaultedTask(ex);
            }
        }
    }
}