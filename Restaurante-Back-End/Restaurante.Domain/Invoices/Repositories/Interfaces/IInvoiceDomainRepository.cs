using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Users.Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        Task<IList<Invoice>> GetAll(Expression<Func<Invoice, bool>> condition, CancellationToken cancellationToken = default);
        Task<PaginationInfo<Invoice>> GetAll(int page, int limit, CancellationToken cancellationToken = default);
        Task<PaginationInfo<Invoice>> GetAll(Expression<Func<Invoice, bool>> condition, int page, int limit, CancellationToken cancellationToken = default);
    }
}
