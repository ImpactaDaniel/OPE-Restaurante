using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Baskets.Requests;
using Restaurante.Application.Common;
using Restaurante.Domain.Baskets.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [Route("[controller]")]
    public class BasketController : APIControllerBase
    {
        public BasketController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpGet("GetActiveBasket/{customerId}"), Produces(typeof(Response<Basket>))]
        public async Task<Response<Basket>> GetActiveBasket(int customerId, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetActiveBasketRequest { CustomerId = customerId }, cancellationToken);
            return response;
        }

        [HttpPost("AddBasketItem/{customerId}"), Produces(typeof(Response<string>))]
        public async Task<Response<string>> AddBasketItem(int customerId, [FromBody] AddBasketItemRequest request, CancellationToken cancellationToken = default)
        {
            request.CustomerId = customerId;
            var response = await _mediator.Send(request, cancellationToken);
            return response;
        }

        [HttpDelete("RemoveBasketItem/{customerId}/{productId}"), Produces(typeof(Response<string>))]
        public async Task<Response<string>> RemoveItem(int customerId, int productId, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new RemoveBasketItemRequest { CustomerId = customerId, ProductId = productId }, cancellationToken);
            return response;
        }
    }
}
