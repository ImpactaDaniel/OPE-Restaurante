using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Common.Requests.Auth
{
    public class RenewTokenRequest : AuthenticationRequest<RenewTokenRequest>, IRequest<Response<TokenResponse>>
    {
        public string Token { get; set; }
        internal class RenewTokenRequestHandler : IRequestHandler<RenewTokenRequest, Response<TokenResponse>>
        {
            private readonly IEmployeesService<Employee> _service;
            private readonly ITokenService _tokenService;
            private readonly INotifier _notifier;

            public RenewTokenRequestHandler(
                IEmployeesService<Employee> service,
                ITokenService tokenService,
                INotifier notifier)
            {
                _service = service;
                _tokenService = tokenService;
                _notifier = notifier;
            }
            public async Task<Response<TokenResponse>> Handle(RenewTokenRequest request, CancellationToken cancellationToken)
            {
                var userId = _tokenService.GetIdByToken(request.Token, false);
                if (userId is null)
                {
                    _notifier.AddNotification(NotificationHelper.InvalidCredentials());
                    return null;
                }

                var user = await _service.Get(userId.Value, cancellationToken);

                var token = _tokenService.GenerateToken(user);

                return new Response<TokenResponse>(true, token);
            }
        }
    }
}
