using Restaurante.Domain.Common.Helper;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Funcionarios.Services
{
    internal class FuncionarioService<TFuncionario> : IFuncionarioService<TFuncionario>
        where TFuncionario : Funcionario
    {
        protected readonly INotifier _notifier;
        protected readonly IFuncionarioDomainRepository<TFuncionario> _repository;

        public FuncionarioService(INotifier notifier, Repositories.IFuncionarioDomainRepository<TFuncionario> repository)
        {
            _notifier = notifier;
            _repository = repository;
        }

        public async Task<bool> CreateFuncionario(TFuncionario funcionario, Funcionario usuario, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _repository
                    .Get(usuario.Id, cancellationToken);

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
            catch (Exception e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
        }

        public async Task<bool> Delete(int id, Funcionario usuario, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _repository
                       .Get(usuario.Id, cancellationToken);

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
            catch (Exception e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return false;
            }
        }

        public  async Task<TFuncionario> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var func = await _repository
                    .Get(id, cancellationToken);

                if(func is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Funcionario)));
                    return null;
                }
                return func;
            }
            catch (Exception e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }

        public Task<IList<TFuncionario>> GetAll(CancellationToken cancellationToken = default) =>
            _repository.GetAll(cancellationToken);

        public async Task<TFuncionario> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _repository
                    .Login(email, password);

                if(user is null)
                {
                    _notifier.AddNotification(NotificationHelper.InvalidEmailOrPassword());
                    return null;
                }

                return user;
            }
            catch(Exception e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }
    }
}
