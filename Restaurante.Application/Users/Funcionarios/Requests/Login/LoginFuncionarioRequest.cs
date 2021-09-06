using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Funcionarios.Requests.Login
{
    public class LoginFuncionarioRequest : FuncionarioRequest<LoginFuncionarioRequest>, IRequest<Response<TokenResponse>>
    {
        internal class LoginFuncionarioRequestHandler : IRequestHandler<LoginFuncionarioRequest, Response<TokenResponse>>
        {
            private readonly IFuncionarioService<Funcionario> _service;
            private readonly ITokenService _tokenService;

            public LoginFuncionarioRequestHandler(IFuncionarioService<Funcionario> service, ITokenService tokenService)
            {
                _service = service;
                _tokenService = tokenService;
            }

            public async Task<Response<TokenResponse>> Handle(LoginFuncionarioRequest request, CancellationToken cancellationToken)
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
