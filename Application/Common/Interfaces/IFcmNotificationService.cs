using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IFcmNotificationService
    {
        Task SendNotificationAsync(string fcmToken, string title, string body);
    }
}
