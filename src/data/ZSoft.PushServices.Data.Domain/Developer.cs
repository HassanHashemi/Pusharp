using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ZSoft.PushServices.Data.IdentityConfig;

namespace ZSoft.PushServices.Data.Domain
{
    public class Developer : IdentityUser<long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Developer, long> manager)
        {
            return await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public List<App> Applications { get; set; }
        public DeveloperAccount Account { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Notification> Notifications { get; set; }
    }
}