using Restaurante.Domain.Common.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface INotifier
    {
        IEnumerable<Notification> GetNotifications();
        bool HasNotifications();
        void AddNotification(Notification notification);
        void AddNotificationList(List<Notification> notifications);
    }
}
