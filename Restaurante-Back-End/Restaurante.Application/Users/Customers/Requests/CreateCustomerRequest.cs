using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Customers.Requests
{
    public class CreateCustomerRequest : CustomerRequest<CreateCustomerRequest>, IRequest<Response<bool>>
    {
        internal class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Response<bool>>
        {
            private readonly ICustomersDomainRepository _customersRepository;
            private readonly ILogger<CreateCustomerRequestHandler> _logger;
            private readonly INotifier _notifier;

            public CreateCustomerRequestHandler(ICustomersDomainRepository customersRepository, ILogger<CreateCustomerRequestHandler> logger, INotifier notifier)
            {
                _customersRepository = customersRepository;
                _logger = logger;
                _notifier = notifier;
            }

            public async Task<Response<bool>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = new Customer(request.Name, request.Email, request.Password)
                    {
                        Addresses = new List<CustomerAddress> { new CustomerAddress(request.Address.Street, request.Address.Number, request.Address.District, request.Address.CEP, request.Address.State, request.Address.City) },
                        FirstAccess = false,
                        CreatedDate = DateTime.Now
                    };

                    await _customersRepository.CreateCustomer(customer, cancellationToken);

                    return new Response<bool>(true, true);
                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
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
