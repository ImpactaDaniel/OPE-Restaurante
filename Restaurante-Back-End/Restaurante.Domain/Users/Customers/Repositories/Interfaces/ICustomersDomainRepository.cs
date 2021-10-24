using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Users.Customers.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Customers.Repositories.Interfaces
{
    public interface ICustomersDomainRepository : IDomainRepository<Customer>
    {
        Task<Customer> Login(string email, string password, CancellationToken cancellationToken = default);
        Task<Customer> Get(int id, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        Task<Customer> CreateCustomer(Customer customer, CancellationToken cancellationToken = default);
        Task<IList<Customer>> GetAll(CancellationToken cancellationToken = default);
    }
}
