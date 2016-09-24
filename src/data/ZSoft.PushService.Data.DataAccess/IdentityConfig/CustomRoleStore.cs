using Microsoft.AspNet.Identity.EntityFramework;
using ZSoft.PushServices.Data.IdentityConfig;

namespace ZSoft.PushService.Data.DataAccess.IdentityConfig
{
    public class CustomRoleStore: RoleStore<CustomRole, long, CustomUserRole>
    {
        public CustomRoleStore(DeveloperDbContext context)
            : base(context)
        {
        }
    }
}
