using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.BasicEntities.Requests.Banks.Models;
using Restaurante.Application.BasicEntities.Requests.Common;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [ApiController, Route("[controller]")]
    public class BanksController : APIControllerBase
    {
        public BanksController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost, Route("Create"), Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEntityRequest<CreateBankRequest, Bank> request, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(request, cancellationToken);
            return GetResponse(res);
        }

        [HttpGet, Route("GetAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new GetAllEntitiesRequest<Bank>(), cancellationToken);
            return GetResponse(res);
        }
    }
}
