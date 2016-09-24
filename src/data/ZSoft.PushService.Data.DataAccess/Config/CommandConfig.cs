using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class CommandConfig: EntityTypeConfiguration<Command>
    {
        public CommandConfig()
        {
            this.HasKey(c => c.id);
        }
    }
}
