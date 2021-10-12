using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.Products.Requests.Create;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [Route("[controller]")]
    public class ProductsController : APIControllerBase
    {
        public ProductsController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost, Route("Create")]
        public async Task<IActionResult> CreateNewProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            request.CurrentUserId = GetLoggedUserId();
            var response = await _mediator.Send(request, cancellationToken);
            return GetResponse(response);
        }
    }
}
