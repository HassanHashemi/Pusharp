using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class DeveloperConfig: EntityTypeConfiguration<Developer>
    {
        public DeveloperConfig()
        {
            this.HasKey(d => d.Id);
            this.HasMany(d => d.Applications)
                    .WithRequired(a => a.Developer)
                        .HasForeignKey(a => a.DeveloperId);

            this.HasOptional(d => d.Account).WithRequired(a => a.Developer);
        }
    }
}
