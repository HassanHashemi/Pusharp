using System.Collections.Generic;

namespace ZSoft.PushServices.Data.Domain
{
    public class NotificationButton
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public List<Command> Commands { get; set; }
        public Notification Notification { get; set; }
        public long NotificationId { get; set; }
    }
}