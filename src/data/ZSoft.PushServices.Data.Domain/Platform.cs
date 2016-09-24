using System.Collections.Generic;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushServices.Data
{
    public class Platform
    {
        public long Id { get; set; }
        public PlatformName Name { get; set; }
        public string Version { get; set; }
        public List<Device> Devices { get; set; }
    }
}