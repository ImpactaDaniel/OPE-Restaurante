using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Employees.Repositories
{
    public interface IEmployeeDomainRepository<TEmployee> : IDomainRepository<TEmployee>
        where TEmployee : Employee
    {
        Task<TEmployee> Login(string email, string password, CancellationToken cancellationToken = default);
        Task<TEmployee> Get(int id, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        Task<TEmployee> CreateEmployee(TEmployee employee, CancellationToken cancellationToken = default);
        Task<IList<TEmployee>> GetAll(CancellationToken cancellationToken = default);
        Task<PaginationInfo<TEmployee>> GetAll(Expression<Func<TEmployee, bool>> condition, int page, int limit, CancellationToken cancellationToken = default);
        Task<PaginationInfo<TEmployee>> GetAll(int page, int limit, CancellationToken cancellationToken = default);
    }
}
