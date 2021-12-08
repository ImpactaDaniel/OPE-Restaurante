using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Encrypt.Intefaces;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Domain.Users.Customers.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Users.Customers
{
    public class CustomersRepository : DataRepository<IRestauranteDbContext, Customer>, ICustomersDomainRepository
    {
        private readonly IPasswordEncrypt _passwordEncrypt;
        public CustomersRepository(IRestauranteDbContext db, IPasswordEncrypt passwordEncrypt) : base(db)
        {
            _passwordEncrypt = passwordEncrypt;
        }

        public async Task<Customer> CreateCustomer(Customer customer, CancellationToken cancellationToken = default)
        {
            var exists = await 
                Data
                .Customers
                .AnyAsync(c => c.Email == customer.Email || c.Document == customer.Document, cancellationToken);

            if (exists)
                throw new BasicTableException("Cliente já existe!", Domain.Common.Enums.NotificationKeys.Error);

            customer.UpdatePassword(customer.Password, _passwordEncrypt.Encrypt(customer.Password));
            await Data.Customers.AddAsync(customer, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return customer;
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Customer> Get(int id, CancellationToken cancellationToken = default) =>
            await All()
                    .Include(c => c.Addresses)
                    .Include(c => c.Phone)
                    .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);                    

        public Task<IList<Customer>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Customer> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await All()
                .Include(c => c.Phone)
                .Include(c => c.Addresses)
                .FirstAsync(c => c.Email == email, cancellationToken);

            if (user is null)
                return null;

            if(_passwordEncrypt.Compare(user.Password, password))            
                return user;

            return null;
        }
    }
}
