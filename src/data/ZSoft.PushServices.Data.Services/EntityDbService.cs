using ZSoft.PushService.Data.DataAccess;

namespace ZSoft.PushServices.Data.Services
{
    public abstract class EntityDbService
    {
        private DeveloperDbContext context;

        public EntityDbService(DeveloperDbContext context)
        {
            this.Context = context;
        }
        
        public DeveloperDbContext Context
        {
            get
            {
                return context;
            }
            private set
            {
                context = value;
            }
        }
    }
}
