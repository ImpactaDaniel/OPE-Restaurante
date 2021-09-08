using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using System.Collections.Generic;
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
        Task<TEmployee> CreateEmployee(TEmployee employee, Employee currentUser, CancellationToken cancellationToken = default);
        Task<IList<TEmployee>> GetAll(CancellationToken cancellationToken = default);
    }
}
