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
    public class EmployeesController : APIControllerBase
    {
        public EmployeesController(IMediator mediatr, INotifier notifier)
            : base(notifier, mediatr)
        {
        }

        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(new GetAllEmployeesRequest(), cancellationToken);
            return GetResponse(resp.Result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNew([FromBody]CreateEmployeeRequest request, CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(request, cancellationToken);
            return GetResponse(resp.Result);
        }
    }
}
