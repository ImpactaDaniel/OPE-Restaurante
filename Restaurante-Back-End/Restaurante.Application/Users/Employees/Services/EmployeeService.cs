using Microsoft.Extensions.Logging;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Users.Funcionarios.Services
{
    internal class EmployeeService<TEmployee> : IEmployeesService<TEmployee>
        where TEmployee : Employee
    {
        protected readonly INotifier _notifier;
        protected readonly IEmployeeDomainRepository<TEmployee> _repository;
        protected readonly ILogger<EmployeeService<TEmployee>> _logger;

        public EmployeeService(INotifier notifier, IEmployeeDomainRepository<TEmployee> repository, ILogger<EmployeeService<TEmployee>> logger)
        {
            _notifier = notifier;
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> CreateEmployee(TEmployee funcionario, int currentUserId, CancellationToken cancellationToken = default)
        {
            try
            {
                //var user = new Funcionario("Teste", "teste@teste.com", "teste123", TiposFuncionario.Gerente);
                var user = await _repository
                    .Get(currentUserId, cancellationToken);

                var employeeExist = await _repository.Get(e => e.Email == funcionario.Email || e.Document == funcionario.Document, cancellationToken);

                if (user is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Employee)));
                    return false;
                }

                if (employeeExist is not null)
                {
                    _notifier.AddNotification(new Notification((int)NotificationKeys.Error, "Funcionário já existe!"));
                    return false;
                }

                if (user.Type != UsersType.Manager)
                {
                    _notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Employee), "criar novo funcionário!"));
                    return false;
                }
                funcionario.CreatedDate = DateTime.Now;
                funcionario.FirstAccess = true;
                await _repository.CreateEmployee(funcionario, cancellationToken);
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
                throw new Exception("Houve um erro ao tentar criar o funcionário!");
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
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Employee)));
                    return false;
                }

                if (user.Type != UsersType.Manager)
                {
                    _notifier.AddNotification(NotificationHelper.DoesntHavePermission(nameof(Employee), "deletar funcionário!"));
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
                throw new Exception("Houve um erro ao tentar deletar o funcionário!");
            }
        }

        public async Task<TEmployee> Get(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var func = await _repository
                    .Get(id, cancellationToken);

                if (func is null)
                {
                    _notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(Employee)));
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
                throw new Exception("Houve um erro ao tentar buscar o funcionário!");
            }
        }

        public Task<IList<TEmployee>> GetAll(CancellationToken cancellationToken = default) =>
            _repository.GetAll(cancellationToken);

        public async Task<PaginationInfo<TEmployee>> GetAll(int page, int limit, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.GetAll(page, limit, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar buscar funcionários!");
            }
        }

        public async Task<TEmployee> Login(string email, string password, CancellationToken cancellationToken = default)
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
                throw new Exception("Houve um erro ao tentar buscar funcionários!");
            }
        }

        public async Task<PaginationInfo<TEmployee>> Search(Expression<Func<TEmployee, bool>> condition, int page, int limit, CancellationToken cancellationToken = default)
        {
            try
            {
                return await _repository.GetAll(condition, page, limit, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new Exception("Houve um erro ao tentar buscar funcionários!");
            }
        }

        public async Task<bool> Update(TEmployee employee, CancellationToken cancellationToken = default)
        {
            return await _repository.Save(employee, cancellationToken);
        }
    }
}
