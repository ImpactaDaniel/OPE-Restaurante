using Microsoft.AspNetCore.Mvc;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common;

namespace Restaurante.Web.Controllers
{
    public abstract class APIControllerBase : Controller
    {
        private readonly INotifier _notifier;

        protected APIControllerBase(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected IActionResult GetResponse<T>(T data, object paginationInfo = null)
        {
            if (!_notifier.HasNotifications())
                return Ok(new DefaultApiResponse<T>(response: data));
            var notifications = _notifier.GetNotifications();
            return BadRequest(new DefaultApiResponse<T>(notifications: notifications, success: false));
        }
    }
}
