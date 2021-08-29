using MediatR;
using Restaurante.Application.Common;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.GetAll
{
    public class GetAllFuncionariosRequest : IRequest<Response<IList<Funcionario>>>
    {
        internal class GetAllFuncionariosRequestHandler : IRequestHandler<GetAllFuncionariosRequest, Response<IList<Funcionario>>>
        {
            private readonly IFuncionarioService<Funcionario> _service;

            public GetAllFuncionariosRequestHandler(IFuncionarioService<Funcionario> service)
            {
                _service = service;
            }

            public async Task<Response<IList<Funcionario>>> Handle(GetAllFuncionariosRequest request, CancellationToken cancellationToken)
            {
                return new Response<IList<Funcionario>>(true, await _service.GetAll());
            }
        }
    }
}
