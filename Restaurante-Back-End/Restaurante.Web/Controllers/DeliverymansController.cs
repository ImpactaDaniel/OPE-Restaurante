using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Users.Deliveries.Requests.Create;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [ApiController, Route("[controller]")]
    public class DeliverymansController : APIControllerBase
    {
        public DeliverymansController(IMediator mediatr, INotifier notifier)
            : base(notifier, mediatr)
        {
        }
        [Route("Create"), HttpPost, Authorize]
        public async Task<IActionResult> CreateNew([FromBody] CreateDeliverymanRequest request, CancellationToken cancellationToken = default)
        {
            var resp = await _mediator.Send(request, cancellationToken);
            return GetResponse(resp.Result);
        }
    }
}
