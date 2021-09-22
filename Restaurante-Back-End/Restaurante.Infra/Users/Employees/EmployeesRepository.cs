using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Users.Employees
{
    internal class EmployeesRepository :
        DataRepository<IRestauranteDbContext, Employee>,
        IEmployeeDomainRepository<Employee>
    {
        public EmployeesRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<Employee> CreateEmployee(Employee funcionario, Employee usuario, CancellationToken cancellationToken = default)
        {
            await Data.Employees.AddAsync(funcionario, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return funcionario;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var entity = await
                All()
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            Data.Employees.Remove(entity);
            await Data.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Employee> Get(int id, CancellationToken cancellationToken = default)
        {
            var entity = await
                All()
                    .AsNoTrackingWithIdentityResolution()
                .Include(e => e.Account)
                .ThenInclude(a => a.Bank)
                .Include(e => e.Address)
                .Include(e => e.Phones)
                .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

            return entity;
        }

        public async Task<IList<Employee>> GetAll(CancellationToken cancellationToken = default) =>
            await All()
                .AsNoTrackingWithIdentityResolution()
                .Include(e => e.Account)
                    .ThenInclude(a => a.Bank)
                .Include(e => e.Address)
                .Include(e => e.Phones)
                .ToListAsync(cancellationToken);

        public async Task<Employee> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await All()
                .FirstOrDefaultAsync(u =>
                    u.Email == email &&
                    u.Password == password,
                    cancellationToken);

            return user;
        }
    }
}
