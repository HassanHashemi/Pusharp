using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class DeveloperAccountConfig: EntityTypeConfiguration<DeveloperAccount>
    {
        public DeveloperAccountConfig()
        {
            this.HasKey(da => da.Id);   
        }
    }
}
