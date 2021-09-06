using Microsoft.Extensions.Logging;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Entregadores.Services
{
    internal class EntregadorService : IEntregadoresService
    {
        private readonly INotifier _notifier;
        private readonly ILogger<EntregadorService> _logger;
        private readonly IFuncionarioDomainRepository<Funcionario> _funcionarioRepository;
        private readonly IEntregadorIntegrationService _entregadorIntegrationService;
        private readonly IMapper<Entregador, EntregadorIntegration> _mapper;

        public EntregadorService(INotifier notifier,
                                 ILogger<EntregadorService> logger,
                                 IFuncionarioDomainRepository<Funcionario> funcionarioDomainRepository,
                                 IEntregadorIntegrationService entregadorIntegrationService,
                                 IMapper<Entregador, EntregadorIntegration> mapper)
        {
            _notifier = notifier;
            _logger = logger;
            _funcionarioRepository = funcionarioDomainRepository;
            _entregadorIntegrationService = entregadorIntegrationService;
            _mapper = mapper;
        }

        public async Task<bool> CreateFuncionario(Entregador funcionario, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await
                            _funcionarioRepository
                            .Get(currentUserId, cancellationToken);

                if(user is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return false;
                }

                if(user.Type != TiposFuncionario.Gerente)
                {
                    _notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Funcionario), "criar novo Entregador!"));
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

        public Task<Entregador> Get(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Entregador>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Entregador> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
