using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Funcionarios.Requests.Login;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common;
using Restaurante.Web.Models;
using System.Threading;
using System.Threading.Tasks;

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

        protected IActionResult GetResponse<T>(T data, object paginationInfo = null)
        {
            if (!_notifier.HasNotifications())
                return Ok(new DefaultApiResponse<T>(response: data));
            var notifications = _notifier.GetNotifications();
            return BadRequest(new DefaultApiResponse<T>(notifications: notifications, success: false));
        }

        [HttpPost, Route("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]LoginModel loginModel, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new LoginFuncionarioRequest() { Email = loginModel.Email, Password = loginModel.Password }, cancellationToken);
            return GetResponse(response);
        }
    }
}
