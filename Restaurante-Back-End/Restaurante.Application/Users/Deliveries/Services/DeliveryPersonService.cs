using Microsoft.Extensions.Logging;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Deliveries.Services
{
    internal class DeliveryPersonService : IDeliveryPersonService
    {
        private readonly INotifier _notifier;
        private readonly ILogger<DeliveryPersonService> _logger;
        private readonly IEmployeeDomainRepository<Employee> _funcionarioRepository;
        private readonly IEntregadorIntegrationService _entregadorIntegrationService;
        private readonly IMapper<DeliveryPerson, DeliveryPersonIntegration> _mapper;

        public DeliveryPersonService(INotifier notifier,
                                 ILogger<DeliveryPersonService> logger,
                                 IEmployeeDomainRepository<Employee> employeeDomainRepository,
                                 IEntregadorIntegrationService entregadorIntegrationService,
                                 IMapper<DeliveryPerson, DeliveryPersonIntegration> mapper)
        {
            _notifier = notifier;
            _logger = logger;
            _funcionarioRepository = employeeDomainRepository;
            _entregadorIntegrationService = entregadorIntegrationService;
            _mapper = mapper;
        }

        public async Task<bool> CreateEmployee(DeliveryPerson funcionario, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await
                            _funcionarioRepository
                            .Get(currentUserId, cancellationToken);

                if(user is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Employee)));
                    return false;
                }

                if(user.Type != EmployeesType.Manager)
                {
                    _notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Employee), "criar novo Entregador!"));
                    return false;
                }

                var integration = _mapper.Map(funcionario);

                await _entregadorIntegrationService.CreateNewEntregador(integration, cancellationToken);

                return true;
            }
            catch(UserException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return false;
            }
        }

        public Task<bool> Delete(int id, int currentUserId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryPerson> Get(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<DeliveryPerson>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<DeliveryPerson> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
