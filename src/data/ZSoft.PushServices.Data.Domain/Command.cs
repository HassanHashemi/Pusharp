namespace ZSoft.PushServices.Data.Domain
{
    public class Command
    {
        public long id { get; set; }
        public CommandType CommandType { get; set; }
        public string URI { get; set; }

        public Notification Notification { get; set; }
        public long? NotificationId { get; set; }

        public NotificationButton Button { get; set; }
        public long? ButtonId { get; set; }
        public string Data { get; set; }
    }
}