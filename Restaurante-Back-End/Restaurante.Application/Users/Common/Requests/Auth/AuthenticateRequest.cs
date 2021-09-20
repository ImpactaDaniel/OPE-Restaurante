using MediatR;
using Restaurante.Application.Common;
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
    public class AuthenticateRequest : AuthenticationRequest<AuthenticateRequest>, IRequest<Response<TokenResponse>>
    {
        internal class AutenticateEmployeeRequestHandler : IRequestHandler<AuthenticateRequest, Response<TokenResponse>>
        {
            private readonly IEmployeesService<Employee> _service;
            private readonly ITokenService _tokenService;
            private readonly INotifier _notifier;

            public AutenticateEmployeeRequestHandler(
                IEmployeesService<Employee> service,
                ITokenService tokenService,
                INotifier notifier)
            {
                _service = service;
                _tokenService = tokenService;
                _notifier = notifier;
            }

            public async Task<Response<TokenResponse>> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
            {
                var user = await _service.Login(request.Email, request.Password, cancellationToken);
                if (user is null)
                    return new Response<TokenResponse>(false, null);

                var token = _tokenService.GenerateToken(user);
                return new Response<TokenResponse>(true, token);
            }
        }
    }
}
