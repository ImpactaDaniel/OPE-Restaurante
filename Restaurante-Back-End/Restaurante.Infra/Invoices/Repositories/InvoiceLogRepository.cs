using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Invoices.Repositories
{
    public class InvoiceLogRepository : DataRepository<IRestauranteDbContext, InvoiceLog>, IInvoiceLogDomainRepository
    {
        public InvoiceLogRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public Task<InvoiceLog> CreateLog(InvoiceLog employee, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<InvoiceLog>> GetAll(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<InvoiceLog>> GetAllByInvoice(Invoice invoice, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
