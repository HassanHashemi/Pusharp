using System.Collections.Generic;

namespace ZSoft.PushServices.Data.Domain
{
    public class App
    {
        public long Id { get; set; }
        public string AppId { get; set; }
        public string Name { get; set; }
        public string PackageName { get; set; }
        public Developer Developer { get; set; }
        public List<Platform> SupportedPlatforms { get; set; }
        public List<Device> Devices { get; set; }
        public long DeveloperId { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}
