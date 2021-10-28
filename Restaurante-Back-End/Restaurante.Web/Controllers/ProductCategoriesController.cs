﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Application.BasicEntities.Requests.Common;
using Restaurante.Application.BasicEntities.Requests.ProductCategories.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Web.Controllers
{
    [ApiController, Route("[controller]")]
    public class ProductCategoriesController : APIControllerBase
    {
        public ProductCategoriesController(INotifier notifier, IMediator mediator) : base(notifier, mediator)
        {
        }

        [HttpPost, Route("Create"), Authorize]
        public async Task<IActionResult> Create([FromBody] CreateEntityRequest<CreateProductCategoryRequest, ProductCategory> request, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(request, cancellationToken);
            return GetResponse(res);
        }

        [HttpGet, Route("GetAll")]
        public async Task<IActionResult> GetAll(int page, int size, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new GetAllEntitiesRequest<ProductCategory> { Page = page, Limit = size }, cancellationToken);
            return GetResponse(res);
        }

        [HttpGet, Route("Search")]
        public async Task<IActionResult> Search(int page, int size, string value, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new SearchEntitiesRequest<ProductCategory> { Field = "name", Value = value, Page = page, Limit = size }, cancellationToken);
            return GetResponse(res);
        }

        [HttpGet, Route("Get/{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken = default)
        {
            var res = await _mediator.Send(new GetEntityRequest<ProductCategory> { Id = id }, cancellationToken);
            return GetResponse(res);
        }
    }
}
