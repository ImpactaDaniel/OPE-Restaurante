using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Restaurante.Infra.Common.Services
{
    public class Notifier : INotifier
    {
        private readonly List<Notification> _notifications = new List<Notification>();

        public void AddNotification(Notification notification) =>
            _notifications.Add(notification);

        public void AddNotificationList(List<Notification> notifications) =>
            _notifications.AddRange(notifications);

        public IEnumerable<Notification> GetNotifications() =>
            _notifications;

        public bool HasNotifications() =>
            _notifications.Any();
    }
}
