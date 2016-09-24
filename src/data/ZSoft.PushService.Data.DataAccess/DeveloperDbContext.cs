using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using ZSoft.PushServices.Data;
using ZSoft.PushServices.Data.Domain;
using ZSoft.PushServices.Data.IdentityConfig;

namespace ZSoft.PushService.Data.DataAccess
{
    public class DeveloperDbContext
          : IdentityDbContext<Developer, CustomRole, long, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public static DeveloperDbContext Create()
        {
            return new DeveloperDbContext();
        }

        public DeveloperDbContext() : base("Default")
        {

        }

        public DbSet<App> Apps { get; set; }
        public DbSet<Command> Commands { get; set; }
        public DbSet<DeveloperAccount> DeveloperAccounts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationButton> NotificationButtons { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
