using Microsoft.AspNet.Identity.EntityFramework;

namespace ZSoft.PushServices.Data.IdentityConfig
{
    public class CustomRole: IdentityRole<long, CustomUserRole>
    {
        public CustomRole()
        {

        }

        public CustomRole(string name)
        {

        }
    }
}
