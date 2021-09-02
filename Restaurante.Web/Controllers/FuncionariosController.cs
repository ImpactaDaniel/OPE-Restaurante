using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Funcionarios.Requests.Create;
using Restaurante.Application.Users.Funcionarios.Requests.GetAll;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    public class FuncionariosController : APIControllerBase
    {
        public FuncionariosController(IMediator mediatr, INotifier notifier)
            : base(notifier, mediatr)
        {
        }

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(new GetAllFuncionariosRequest(), cancellationToken);
            return GetResponse(resp.Result);
        }

        [HttpPost, Authorize(Roles = "Gerente")]
        public async Task<IActionResult> CreateNew([FromBody]CreateFuncionarioRequest request, CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(request, cancellationToken);
            return GetResponse(resp.Result);
        }
    }
}
