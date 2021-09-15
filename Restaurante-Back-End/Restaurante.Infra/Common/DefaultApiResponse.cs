using Restaurante.Domain.Common.Models;
using System.Collections.Generic;

namespace Restaurante.Infra.Common
{
    public class DefaultApiResponse<T>
    {
        public IEnumerable<Notification> Notifications { get; private set; }
        public T Response { get; set; }
        public object PaginationInfo { get; private set; }
        public bool Success { get; private set; }
        public DefaultApiResponse(IEnumerable<Notification> notifications = null, T response = default, object paginationInfo = null, bool success = true)
        {
            Notifications = notifications;
            Response = response;
            PaginationInfo = paginationInfo;
            Success = success;
        }
    }
}
