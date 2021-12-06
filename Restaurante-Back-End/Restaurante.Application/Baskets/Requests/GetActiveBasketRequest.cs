using MediatR;
using Restaurante.Application.Baskets.Common.Models;
using Restaurante.Application.Common;
using Restaurante.Domain.Baskets.Models;
using Restaurante.Domain.Baskets.Repositories.Interfaces;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Baskets.Requests
{
    public class GetActiveBasketRequest : BasketRequest, IRequest<Response<Basket>>
    {
        internal class GetActiveBasketRequestHandler : IRequestHandler<GetActiveBasketRequest, Response<Basket>>
        {
            private readonly ICustomersDomainRepository _customerRepository;
            private readonly IBasketRepository _basketRepository;

            public GetActiveBasketRequestHandler(ICustomersDomainRepository customerRepository, IBasketRepository basketRepository)
            {
                _customerRepository = customerRepository;
                _basketRepository = basketRepository;
            }

            public async Task<Response<Basket>> Handle(GetActiveBasketRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = await _customerRepository.Get(request.CustomerId, cancellationToken);

                    if (customer == null)
                        throw new Exception("Cliente não existe!");

                    return new Response<Basket>(true, await _basketRepository.GetActiveBasket(request.CustomerId, cancellationToken));
                }
                catch (Exception)
                {
                    return new Response<Basket>(false, null);
                }
            }
        }
    }
}
