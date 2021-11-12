using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Encrypt.Intefaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Users.Employees
{
    internal class EmployeesRepository :
        DataRepository<IRestauranteDbContext, Employee>,
        IEmployeeDomainRepository<Employee>
    {
        private readonly IPasswordEncrypt _passwordEncrypt;

        public EmployeesRepository(IRestauranteDbContext db, IPasswordEncrypt passwordEncrypt) : base(db)
        {
            _passwordEncrypt = passwordEncrypt;
        }

        public async Task<Employee> CreateEmployee(Employee funcionario, CancellationToken cancellationToken = default)
        {
            funcionario.UpdatePassword(funcionario.Password, _passwordEncrypt.Encrypt(funcionario.Password));
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

            entity.HidePassword();

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

        public async Task<PaginationInfo<Employee>> GetAll(Expression<Func<Employee, bool>> condition, int page, int limit, CancellationToken cancellationToken = default)
        {
            var list = await All()
                        .Where(condition)
                        .Skip(page * limit)
                        .Take(limit)
                        .Include(e => e.Phones)
                        .Include(e => e.Address)
                        .Include(e => e.Account)                        
                            .ThenInclude(a => a.Bank)
                        .ToListAsync(cancellationToken);

            var count = await All()
                        .Where(condition)
                        .CountAsync(cancellationToken);

            return new PaginationInfo<Employee>
            {
                Entities = list,
                Size = count
            };
        }

        public async Task<PaginationInfo<Employee>> GetAll(int page, int limit, CancellationToken cancellationToken = default)
        {
            var list = await All()
                        .Skip(page * limit)
                        .Take(limit)
                        .Include(e => e.Phones)
                        .Include(e => e.Address)
                        .Include(e => e.Account)
                            .ThenInclude(a => a.Bank)
                        .ToListAsync(cancellationToken);

            var count = await All()
                        .CountAsync(cancellationToken);

            return new PaginationInfo<Employee>
            {
                Entities = list,
                Size = count
            };
        }

        public async Task<Employee> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await All()
                .Include(e => e.Phones)
                .Include(e => e.Address)
                .Include(e => e.Account)
                    .ThenInclude(a => a.Bank)
                .FirstOrDefaultAsync(u =>
                    u.Email == email,
                    cancellationToken);

            if (!_passwordEncrypt.Compare(user?.Password, password))
                return null;

            return user;
        }

        public override async Task<bool> Save(Employee entity, CancellationToken cancellationToken = default)
        {
            entity.UpdatePassword(_passwordEncrypt.Encrypt(entity.Password));
            return await base.Save(entity, cancellationToken);
        }
    }
}
