using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Customers.Requests;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [Route("[controller]")]
    public class CustomerController : APIControllerBase
    {
        public CustomerController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }


        [HttpPost("CreateCustomer"), AllowAnonymous, Produces(typeof(Response<string>))]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken = default)
        {
            return Ok( await _mediator.Send(request, cancellationToken));
        }
    }
}
