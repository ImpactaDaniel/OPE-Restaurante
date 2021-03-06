using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Funcionarios.Services.Interfaces
{
    public interface IEmployeesService<TEmployee> : IEntityService<TEmployee>
        where TEmployee : Employee
    {
        Task<bool> CreateEmployee(TEmployee employee, int currentUserId, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, int currentUserId, CancellationToken cancellationToken = default);
        Task<TEmployee> Login(string email, string password, CancellationToken cancellationToken = default);
        Task<IList<TEmployee>> GetAll(CancellationToken cancellationToken = default);
        Task<bool> Update(TEmployee employee, CancellationToken cancellationToken = default);
        Task<PaginationInfo<TEmployee>> Search(Expression<Func<TEmployee, bool>> condition, int page, int limit, CancellationToken cancellationToken = default);
        Task<PaginationInfo<TEmployee>> GetAll(int page, int limit, CancellationToken cancellationToken = default);
    }
}
