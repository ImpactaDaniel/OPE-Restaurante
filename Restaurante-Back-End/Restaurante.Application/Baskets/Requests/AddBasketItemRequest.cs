using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Baskets.Common.Models;
using Restaurante.Application.Common;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Baskets.Repositories.Interfaces;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Baskets.Requests
{
    public class AddBasketItemRequest : BasketItemRequest, IRequest<Response<string>>
    {
        public int CustomerId { get; set; }

        internal class AddBasketItemRequestHandler : IRequestHandler<AddBasketItemRequest, Response<string>>
        {
            private readonly IProductDomainRepository _productRepository;
            private readonly ICustomersDomainRepository _customerRepository;
            private readonly IBasketRepository _basketRepository;
            private readonly ILogger<AddBasketItemRequestHandler> _logger;

            public AddBasketItemRequestHandler(
                IProductDomainRepository productRepository,
                ICustomersDomainRepository customerRepository,
                IBasketRepository basketRepository,
                ILogger<AddBasketItemRequestHandler> logger)
            {
                _productRepository = productRepository;
                _customerRepository = customerRepository;
                _basketRepository = basketRepository;
                _logger = logger;
            }

            public async Task<Response<string>> Handle(AddBasketItemRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = await _customerRepository.Get(request.CustomerId, cancellationToken);
                    if (customer == null)
                        throw new BasicTableException("Cliente não encontrado!", NotificationKeys.Error);

                    var product = await _productRepository.Get(p => p.Id == request.ProductId, cancellationToken);

                    if (product == null)
                        throw new BasicTableException("Produto não encontrado!", NotificationKeys.Error);

                    await _basketRepository.AddOrUpdateItem(request.CustomerId, new Domain.Baskets.Models.BasketItem
                    {
                        Obs = request.Obs,
                        Product = product,
                        Quantity = request.Quantity
                    }, cancellationToken);

                    return new Response<string>(true, "Atualizado com sucesso!");
                }
                catch (RestauranteException e)
                {
                    return new Response<string>(false, e.Message);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    return new Response<string>(false, e.Message);
                }
            }
        }
    }
}
