using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Users.Customers.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Invoices.Repositories.Interfaces
{
    public interface IInvoiceDomainRepository : IDomainRepository<Invoice>
    {
        Task<Invoice> CreateInvoice(Invoice invoice, CancellationToken cancellationToken = default);
        Task<bool> Delete(Invoice invoice, CancellationToken cancellationToken = default);
        Task<IList<Invoice>> GetAll(CancellationToken cancellationToken = default);
        Task<IList<Invoice>> GetAll(Customer customer, CancellationToken cancellationToken = default);
    }
}
