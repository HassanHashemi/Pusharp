
using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class PlatformConfig: EntityTypeConfiguration<Platform>
    {
        public PlatformConfig()
        {
            this.HasKey(p => p.Id);

            this.HasMany(p => p.Devices).WithRequired(d => d.Platform).HasForeignKey(d => d.PlatformId);
        }
    }
}
