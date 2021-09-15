using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Employees.Requests.Login
{
    public class AutenticateEmployeeRequest : EmployeeRequest<AutenticateEmployeeRequest>, IRequest<Response<TokenResponse>>
    {
        internal class AutenticateEmployeeRequestHandler : IRequestHandler<AutenticateEmployeeRequest, Response<TokenResponse>>
        {
            private readonly IEmployeesService<Employee> _service;
            private readonly ITokenService _tokenService;

            public AutenticateEmployeeRequestHandler(IEmployeesService<Employee> service, ITokenService tokenService)
            {
                _service = service;
                _tokenService = tokenService;
            }

            public async Task<Response<TokenResponse>> Handle(AutenticateEmployeeRequest request, CancellationToken cancellationToken)
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
