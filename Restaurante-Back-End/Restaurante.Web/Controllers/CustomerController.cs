using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Customers.Requests;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Customers.Models;
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

        [HttpPost("LoginCustomer"), AllowAnonymous, Produces(typeof(Response<Customer>))]
        public async Task<IActionResult> LoginCustomer([FromBody] GetUserAuthenticateRequest request, CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(request, cancellationToken));
        }

        [HttpGet("GetCustomerById/{customerId}"), Produces(typeof(Response<Customer>))]
        public async Task<IActionResult> GetCustomerById(int customerId, CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(new GetCustomerByIdRequest { Id = customerId }, cancellationToken));
        }
    }
}
