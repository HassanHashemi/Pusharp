using Microsoft.AspNet.Identity.EntityFramework;
using ZSoft.PushServices.Data.Domain;
using ZSoft.PushServices.Data.IdentityConfig;

namespace ZSoft.PushService.Data.DataAccess.IdentityConfig
{
    public class CustomUserStore : UserStore<Developer, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public CustomUserStore(DeveloperDbContext context) : base(context)
        {
        }
    }
}
