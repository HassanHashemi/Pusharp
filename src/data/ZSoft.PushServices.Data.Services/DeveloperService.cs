using System.Threading.Tasks;
using ZSoft.PushServices.Data.Domain;
using System.Linq;
using System;
using ZSoft.PushServices.Data.Services.Abstraction;
using ZSoft.PushService.Data.DataAccess;

namespace ZSoft.PushServices.Data.Services
{
    public class DeveloperDbService : EntityDbService, IDeveloperDbService
    {
        public DeveloperDbService(): base(DeveloperDbContext.Create())
        {
        }
        
        public App[] GetApps(long developerId)
        {
            return this.Context.Apps.Where(a => a.DeveloperId == developerId).ToArray();
        }
        
        public async Task<string> RegisterAppAsync(App app)
        {
            app.AppId = Guid.NewGuid().ToString();
            this.Context.Apps.Add(app);
            await this.Context.SaveChangesAsync();
            return app.AppId;
        }

        public Task<int> SaveNotificationAsync(Notification notification)
        {
            this.Context.Notifications.Add(notification);
            return this.Context.SaveChangesAsync();
        }
    }
}
