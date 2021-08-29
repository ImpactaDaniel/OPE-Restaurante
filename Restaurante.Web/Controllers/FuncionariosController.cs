using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Create;
using Restaurante.Application.Users.GetAll;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly IMediator _mediatr;

        public FuncionariosController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var resp = await _mediatr.Send(new GetAllFuncionariosRequest(), cancellationToken);
            return Json(new { data = resp.Result });
        }

        public async Task<IActionResult> CreateNew([FromBody]CreateFuncionarioRequest request, CancellationToken cancellationToken = default)
        {
            var resp = await _mediatr.Send(request, cancellationToken);
        }
    }
}
