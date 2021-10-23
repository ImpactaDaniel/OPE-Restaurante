using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Invoices.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Invoices.Repositories.Interfaces
{
    public interface IInvoiceLogDomainRepository : IDomainRepository<InvoiceLog>
    {
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        Task<InvoiceLog> CreateLog(InvoiceLog log, CancellationToken cancellationToken = default);
        Task<IList<InvoiceLog>> GetAll(CancellationToken cancellationToken = default);
        Task<IList<InvoiceLog>> GetAllByInvoice(Invoice invoice, CancellationToken cancellationToken = default);
    }
}
