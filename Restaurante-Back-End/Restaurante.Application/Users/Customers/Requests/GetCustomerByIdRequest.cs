using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Customers.Requests
{
    public class GetCustomerByIdRequest : IRequest<Response<Customer>>
    {
        public int Id { get; set; }

        internal class GetCustomerByIdRequestHandler : IRequestHandler<GetCustomerByIdRequest, Response<Customer>>
        {
            private readonly ICustomersDomainRepository _customersRepository;
            private readonly ILogger<GetCustomerByIdRequestHandler> _logger;

            public GetCustomerByIdRequestHandler(ICustomersDomainRepository customersRepository, ILogger<GetCustomerByIdRequestHandler> logger)
            {
                _customersRepository = customersRepository;
                _logger = logger;
            }

            public async Task<Response<Customer>> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _customersRepository.Get(request.Id, cancellationToken) ;
                    if (user == null)
                        throw new BasicTableException("Usuario não existe!", Domain.Common.Enums.NotificationKeys.Error);
                    return new Response<Customer>(true, user);
                }
                catch (RestauranteException)
                {
                    return new Response<Customer>(false, null);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception("Houve um erro ao criar cliente!");
                }
            }
        }
    }
}
