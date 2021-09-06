using Microsoft.Extensions.Logging;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Funcionarios.Services
{
    internal class FuncionarioService<TFuncionario> : IFuncionarioService<TFuncionario>
        where TFuncionario : Funcionario
    {
        protected readonly INotifier _notifier;
        protected readonly IFuncionarioDomainRepository<TFuncionario> _repository;
        protected readonly ILogger<FuncionarioService<TFuncionario>> _logger;

        public FuncionarioService(INotifier notifier, IFuncionarioDomainRepository<TFuncionario> repository, ILogger<FuncionarioService<TFuncionario>> logger)
        {
            _notifier = notifier;
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> CreateFuncionario(TFuncionario funcionario, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                //var user = new Funcionario("Teste", "teste@teste.com", "teste123", TiposFuncionario.Gerente);
                var user = await _repository
                    .Get(currentUserId, cancellationToken);

                if (user is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return false;
                }

                if (user.Type != TiposFuncionario.Gerente)
                {
                    _notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Funcionario), "criar novo funcionário!"));
                    return false;
                }
                funcionario.CreatedDate = DateTime.Now;
                await _repository.CreateFuncionario(funcionario, user, cancellationToken);
                return true;

            }
            catch (UserException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar criar o funcionário!", e);
            }
        }

        public async Task<bool> Delete(int id, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _repository
                       .Get(currentUserId, cancellationToken);

                if (user is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return false;
                }

                if (user.Type != TiposFuncionario.Gerente)
                {
                    _notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Funcionario), "deletar funcionário!"));
                    return false;
                }
                await _repository.Delete(id, cancellationToken);
                return true;
            }
            catch (UserException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar deletar o funcionário!", e);
            }
        }

        public async Task<TFuncionario> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var func = await _repository
                    .Get(id, cancellationToken);

                if (func is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return null;
                }
                return func;
            }
            catch (UserException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar buscar o funcionário!", e);
            }
        }

        public Task<IList<TFuncionario>> GetAll(CancellationToken cancellationToken = default) =>
            _repository.GetAll(cancellationToken);

        public async Task<TFuncionario> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _repository
                    .Login(email, password, cancellationToken);

                if (user is null)
                {
                    _notifier.AddNotification(NotificationHelper.InvalidEmailOrPassword());
                    return null;
                }

                return user;
            }
            catch (UserException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar buscar funcionários!", e);
            }
        }
    }
}
