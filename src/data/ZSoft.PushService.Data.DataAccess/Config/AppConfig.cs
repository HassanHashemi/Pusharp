using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class AppConfig: EntityTypeConfiguration<App>
    {
        public AppConfig()
        {
            this.HasKey(a => a.Id);

            this.HasRequired(a => a.Developer)
                    .WithMany(d => d.Applications)
                        .HasForeignKey(a => a.DeveloperId)
                            .WillCascadeOnDelete(true);

            this.HasMany(a => a.Devices)
                    .WithOptional(dev => dev.App)
                        .HasForeignKey(d => d.AppId)
                            .WillCascadeOnDelete(false);

            this.HasMany(a => a.Notifications)
                .WithRequired(n => n.App)
                    .HasForeignKey(n => n.AppId)
                        .WillCascadeOnDelete(true);
        }
    }
}
