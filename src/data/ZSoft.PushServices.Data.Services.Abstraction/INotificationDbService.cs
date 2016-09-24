using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSoft.PushServices.Data.Domain;

namespace ZSoft.PushServices.Data.Services
{
    public interface INotificationDbService
    {
        Task<int> AddNotificationAsync(Notification notification);
    }
}
