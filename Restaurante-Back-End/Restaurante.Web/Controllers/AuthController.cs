using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Common.Requests.Auth;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Web.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [ApiController, Route("[controller]")]
    public class AuthController : APIControllerBase
    {
        public AuthController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost, Route("ChangePassword"), Authorize]
        public async Task<IActionResult> ChangePasswordFirstAccess([FromBody] ChangePasswordModel changePasswordModel, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new ChangePasswordFirstAccessRequest { Id = GetLoggedUserId(), OldPassword = changePasswordModel.OldPassword, Password = changePasswordModel.NewPassword}, cancellationToken);

            if (!response.Success)
                return GetResponse(response);

            var token = await _mediator.Send(new AuthenticateRequest { Email = response.Result.Email, Password = changePasswordModel.NewPassword }, cancellationToken);

            return GetResponse(token);
        }

        [HttpPost, Route("Authenticate"), AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel loginModel, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new AuthenticateRequest() { Email = loginModel.Email, Password = loginModel.Password }, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("RenewToken")]
        public async Task<IActionResult> RenewToken(CancellationToken cancellationToken = default)
        {
            var token = Request.Headers["Authorization"].ToString();
            token = token.Replace("Bearer ", string.Empty);
            var response = await _mediator.Send(new RenewTokenRequest() { Token = token }, cancellationToken);
            return GetResponse(response);
        }
    }
}
