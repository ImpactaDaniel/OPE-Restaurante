using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using System.Collections.Generic;
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
    }
}
