using System.Threading.Tasks;
using ZSoft.PushService.Data.DataAccess;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushServices.Data.Services
{
    public class NotificationDbService : EntityDbService, INotificationDbService
    {
        public NotificationDbService(): base(DeveloperDbContext.Create())
        {
        }

        public Task<int> AddNotificationAsync(Notification notification)
        {
            this.Context.Notifications.Add(notification);
            return this.Context.SaveChangesAsync();
        }
    }
}
