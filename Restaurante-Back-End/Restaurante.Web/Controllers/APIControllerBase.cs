using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common;
using System.Linq;
using System.Security.Claims;

namespace Restaurante.Web.Controllers
{
    public abstract class APIControllerBase : ControllerBase
    {
        private readonly INotifier _notifier;
        protected readonly IMediator _mediator;
        protected APIControllerBase(INotifier notifier, IMediator mediator)
        {
            _notifier = notifier;
            _mediator = mediator;
        }

        protected IActionResult GetResponse<T>(T data)
        {
            if (!_notifier.HasNotifications())
                return Ok(new DefaultApiResponse<T>(response: data));
            var notifications = _notifier.GetNotifications();
            return Ok(new DefaultApiResponse<T>(notifications: notifications, success: false));
        }

        protected int GetLoggedUserId()
        {
            var idUser = User.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
            return int.Parse(idUser);
        }
    }
}
