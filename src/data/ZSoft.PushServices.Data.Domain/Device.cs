
namespace ZSoft.PushServices.Data.Domain
{
    public class Device
    {
        public long BaseId { get; set; }
        public string Id { get; set; }

        public Platform Platform { get; set; }
        public long PlatformId { get; set; }

        public string Model { get; set; }
        public string Manufacturer { get; set; }

        public long? AppId { get; set; }
        public App App { get; set; }
    }
}
