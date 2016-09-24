
using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class DeviceConfig: EntityTypeConfiguration<Device>
    {
        public DeviceConfig()
        {
            this.HasKey(d => d.BaseId);
        }
    }
}
