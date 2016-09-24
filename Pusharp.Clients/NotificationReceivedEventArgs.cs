using Pusharp.Net.DTO;
using System;

namespace Pusharp.Clients
{
    public class NotificationReceivedEventArgs: EventArgs
    {
        public NotificationDTO Notification { get; set; }
    }
}
