using System.Data.Entity.Migrations;

namespace ZSoft.PushService.Data.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DeveloperDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DeveloperDbContext context)
        {
            
        }
    }
}
