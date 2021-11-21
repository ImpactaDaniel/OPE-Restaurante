using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Users.Common.Models;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Customers.Requests
{
    public class GetUserAuthenticateRequest : CustomerRequest<GetUserAuthenticateRequest>, IRequest<Response<Customer>>
    {
        internal class GetUserAuthenticateRequestHandler : IRequestHandler<GetUserAuthenticateRequest, Response<Customer>>
        {
            private readonly ICustomersDomainRepository _customersRepository;
            private readonly ILogger<GetUserAuthenticateRequestHandler> _logger;

            public GetUserAuthenticateRequestHandler(ICustomersDomainRepository customersRepository, ILogger<GetUserAuthenticateRequestHandler> logger)
            {
                _customersRepository = customersRepository;
                _logger = logger;
            }

            public async Task<Response<Customer>> Handle(GetUserAuthenticateRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var user = await _customersRepository.Login(request.Email, request.Password, cancellationToken);
                    if (user == null)
                        throw new BasicTableException("E-mail ou senha inválidos!", Domain.Common.Enums.NotificationKeys.Error);
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
