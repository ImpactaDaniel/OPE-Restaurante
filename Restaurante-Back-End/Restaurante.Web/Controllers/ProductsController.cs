using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Products.Requests.Create;
using Restaurante.Application.Products.Requests.Delete;
using Restaurante.Application.Products.Requests.Get;
using Restaurante.Application.Products.Requests.Update;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        [HttpGet, Route("GetAll"), Produces(typeof(Response<PaginationInfo<ProductResponseDTO>>))]
        public async Task<IActionResult> GetAllProducts(int page, int limit, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetAllProductsRequest() { Page = page, Limit = limit } , cancellationToken);
            return Ok(response);
        }

        [HttpDelete, Route("Delete/{id}"), Authorize]
        public async Task<IActionResult> DeleteProduct(int id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new DeleteProductRequest { Id = id, CurrentUserId = GetLoggedUserId() }, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("Get/{id}"), Produces(typeof(Response<ProductResponseDTO>))]
        public async Task<IActionResult> GetProduct(int id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetProductRequest() { Id = id }, cancellationToken);
            return GetResponse(response);
        }

        [HttpGet, Route("Search"), Produces(typeof(Response<PaginationInfo<ProductResponseDTO>>))]
        public async Task<IActionResult> SearchByName(string field, string value, int page, int limit, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new SearchProductsRequest() { Field = field, Value = value, Page = page, Limit = limit }, cancellationToken);
            return Ok(response);
        }

        [HttpGet("GetGroupByCategories"), Produces(typeof(Response<IEnumerable<ProductCategoryResponseDTO>>))]
        public async Task<IActionResult> GetGroupByCategories(CancellationToken cancellationToken = default)
        {
            return Ok(await _mediator.Send(new GetProductGroupByCategories(), cancellationToken));
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
