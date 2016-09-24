using System.Data.Entity.ModelConfiguration;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushService.Data.DataAccess.Config
{
    public class NotificationConfig: EntityTypeConfiguration<Notification>
    {
        public NotificationConfig()
        {
            this.HasKey(n => n.Id);
            this.HasMany(n => n.Buttons).WithRequired(b => b.Notification).HasForeignKey(n => n.NotificationId).WillCascadeOnDelete(true);
            this.HasMany(n => n.Commands).WithOptional(c => c.Notification).HasForeignKey(c => c.NotificationId).WillCascadeOnDelete(true);
            this.HasRequired(n => n.Sender).WithMany(sd => sd.Notifications).HasForeignKey(n => n.SenderId).WillCascadeOnDelete(false);
        }
    }
}
