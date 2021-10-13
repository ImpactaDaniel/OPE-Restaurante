using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Restaurante.Application.Products.Requests.Create;
using Restaurante.Application.Products.Requests.Get;
using Restaurante.Domain.Common.Services.Interfaces;
using System;
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

        [HttpPost, Authorize, Route("Create")]
        public async Task<IActionResult> CreateNewProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken = default)
        {
            request.CurrentUserId = GetLoggedUserId();
            var response = await _mediator.Send(request, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("GetAll")]
        public async Task<IActionResult> GetAllProducts(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetAllProductsRequest(), cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("{fileName}")]
        public IActionResult GetPhoto(string fileName)
        {

            var path = $@"{AppDomain.CurrentDomain.BaseDirectory}\Product\Photos\{fileName}";
            new FileExtensionContentTypeProvider().TryGetContentType(path, out string contentType);

            return PhysicalFile(path, contentType);
        }
    }
}
