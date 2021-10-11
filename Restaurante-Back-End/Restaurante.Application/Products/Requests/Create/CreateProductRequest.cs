﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Products.Common.Models;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Factories.Interfaces;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Requests.Create
{
    public class CreateProductRequest : ProductRequest<CreateProductRequest>, IRequest<Response<bool>>
    {
        internal class CreateProductRequestHandler : IRequestHandler<CreateProductRequest, Response<bool>>
        {
            private readonly IProductFactory _factory;
            private readonly IProductService _service;
            private readonly INotifier _notifier;
            private readonly IBasicEntitiesService _basicEntitiesService;

            public CreateProductRequestHandler(IProductFactory factory, IProductService service, INotifier notifier, IBasicEntitiesService basicEntitiesService)
            {
                _factory = factory;
                _service = service;
                _notifier = notifier;
                _basicEntitiesService = basicEntitiesService;
            }

            public async Task<Response<bool>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var category = await _basicEntitiesService.GetEntity<ProductCategory>(pc => pc.Id == request.Category.Id, cancellationToken);

                    if(category is null)
                    {
                        _notifier.AddNotification(NotificationHelper.EntityNotFound("Categoria"));
                        return new Response<bool>(false, false);
                    }

                    var product = _factory
                                    .WithAccompaniments(request.Accompaniments)
                                    .WithAvailability(request.Available)
                                    .WithCategory(category)
                                    .WithPhoto(request.Photo.PhotoPath)
                                    .WithPrice(request.Price)
                                    .WithName(request.Name)
                                    .WithDescription(request.Description)
                                    .WithQuantity(request.QuantityStock)
                                    .Build();

                    var success = await _service.CreateProduct(product, request.CurrentUserId, cancellationToken);
                    return new Response<bool>(success, success);
                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
