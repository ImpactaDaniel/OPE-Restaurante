using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Login
{
    public class LoginFuncionarioRequest : FuncionarioRequest<LoginFuncionarioRequest>, IRequest<Response<Funcionario>>
    {
        internal class LoginFuncionarioRequestHandler : IRequestHandler<LoginFuncionarioRequest, Response<Funcionario>>
        {
            private readonly IFuncionarioService<Funcionario> _service;

            public LoginFuncionarioRequestHandler(IFuncionarioService<Funcionario> service)
            {
                _service = service;
            }

            public async Task<Response<Funcionario>> Handle(LoginFuncionarioRequest request, CancellationToken cancellationToken)
            {
                var user = await _service.Login(request.Email, request.Password, cancellationToken);
                return new Response<Funcionario>(true, user);
            }
        }
    }
}
