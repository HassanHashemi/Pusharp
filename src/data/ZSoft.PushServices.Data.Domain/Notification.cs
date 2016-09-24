
using System;
using System.Collections.Generic;

namespace ZSoft.PushServices.Data.Domain
{
    public class Notification
    {
        public long Id { get; set; }
        public Developer Sender { get; set; }
        public long SenderId { get; set; }
        public DateTime PushDate { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public long AppId { get; set; }
        public App App { get; set; }
        public List<Command> Commands { get; set; }
        public List<NotificationButton> Buttons { get; set; }

        public string LargeIconUrl { get; set; }
        public string SmallIconUrl { get; set; }
    }
}
