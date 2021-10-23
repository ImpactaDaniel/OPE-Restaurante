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
        public CustomersRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public Task<Customer> CreateEmployee(Customer customer, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Customer> Get(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<Customer>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Customer> Login(string email, string password, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
