using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Restaurante.Application.Products.Requests.Create;
using Restaurante.Application.Products.Requests.Delete;
using Restaurante.Application.Products.Requests.Get;
using Restaurante.Application.Products.Requests.Update;
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

        [HttpPut, Authorize, Route("Update/{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken = default)
        {
            request.CurrentUserId = GetLoggedUserId();
            request.Id = id;
            var response = await _mediator.Send(request, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("GetAll")]
        public async Task<IActionResult> GetAllProducts(int page, int pageLength, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetAllProductsRequest() { Page = page, Length = pageLength } , cancellationToken);
            return GetResponse(response);
        }

        [HttpDelete, Route("Delete/{id}"), Authorize]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new DeleteProductRequest { Id = id, CurrentUserId = GetLoggedUserId() }, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("Get/{id}")]
        public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetProductRequest() { Id = id }, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("Search/{name}")]
        public async Task<IActionResult> SearchByName(string name, int page, int limit, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new SearchProductsRequest() { Name = name, Page = page, Limit = limit }, cancellationToken);
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
