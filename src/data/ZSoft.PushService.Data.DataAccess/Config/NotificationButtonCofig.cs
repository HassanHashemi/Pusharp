
using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class NotificationButtonCofig: EntityTypeConfiguration<NotificationButton>
    {
        public NotificationButtonCofig()
        {
            this.HasKey(n => n.Id);

            this.HasMany(nb => nb.Commands).WithOptional(c => c.Button).HasForeignKey(c => c.ButtonId);
        }
    }
}
