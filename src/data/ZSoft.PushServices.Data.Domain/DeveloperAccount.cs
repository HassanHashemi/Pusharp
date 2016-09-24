using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushServices.Data
{
    public class DeveloperAccount
    {
        public long Id { get; set; }
        public Developer Developer { get; set; }
        public double Balance { get; set; }
    }
}
