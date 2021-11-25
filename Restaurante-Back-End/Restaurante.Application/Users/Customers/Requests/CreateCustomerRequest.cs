using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
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
    public class CreateCustomerRequest : CustomerRequest<CreateCustomerRequest>, IRequest<Response<string>>
    {
        internal class CreateCustomerRequestHandler : IRequestHandler<CreateCustomerRequest, Response<string>>
        {
            private readonly ICustomersDomainRepository _customersRepository;
            private readonly ILogger<CreateCustomerRequestHandler> _logger;

            public CreateCustomerRequestHandler(ICustomersDomainRepository customersRepository, ILogger<CreateCustomerRequestHandler> logger)
            {
                _customersRepository = customersRepository;
                _logger = logger;
            }

            public async Task<Response<string>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var customer = new Customer(request.Name, request.Email, request.Password, request.Document)
                    {
                        Addresses = new List<CustomerAddress> { new CustomerAddress(request.Address.Street, request.Address.Number, request.Address.District, request.Address.CEP, request.Address.State, request.Address.City) },
                        FirstAccess = false,
                        CreatedDate = DateTime.Now,
                        BirthDate = request.BirthDate,
                        Phone = new CustomerPhone
                        {
                            DDD = request.Phone.DDD,
                            PhoneNumber = request.Phone.PhoneNumber
                        }
                    };

                    await _customersRepository.CreateCustomer(customer, cancellationToken);

                    return new Response<string>(true, "Cliente criado com sucesso!");
                }
                catch (RestauranteException e)
                {
                    return new Response<string>(false, e.Message);
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
