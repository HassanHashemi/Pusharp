using System.Threading.Tasks;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushServices.Data.Services.Abstraction
{
    public interface IDeveloperDbService
    {
        App[] GetApps(long developerId);
        Task<string> RegisterAppAsync(App app);
        Task<int> SaveNotificationAsync(Notification notification);
    }
}
