using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Create;
using Restaurante.Application.Users.GetAll;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    public class FuncionariosController : APIControllerBase
    {
        private readonly IMediator _mediatr;

        public FuncionariosController(IMediator mediatr, INotifier notifier)
            : base(notifier)
        {
            _mediatr = mediatr;
        }

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var resp = await _mediatr.Send(new GetAllFuncionariosRequest(), cancellationToken);
            return GetResponse(resp.Result);
        }

        [HttpPost] 
        public async Task<IActionResult> CreateNew([FromBody]CreateFuncionarioRequest request, CancellationToken cancellationToken = default)
        {
            var resp = await _mediatr.Send(request, cancellationToken);
            return GetResponse(resp.Result);
        }
    }
}
