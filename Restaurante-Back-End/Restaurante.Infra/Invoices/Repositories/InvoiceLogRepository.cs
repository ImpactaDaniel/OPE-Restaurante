using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Restaurante.Infra.Invoices.Repositories
{
    public class InvoiceLogRepository : DataRepository<IRestauranteDbContext, InvoiceLog>, IInvoiceLogDomainRepository
    {
        public InvoiceLogRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<InvoiceLog> CreateLog(InvoiceLog log, CancellationToken cancellationToken = default)
        {
            await Data.InvoiceLogs.AddAsync(log, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return log;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            var log = await Data.InvoiceLogs.FirstAsync(l => l.Id == id, cancellationToken);
            Data.InvoiceLogs.Remove(log);
            return await Data.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IList<InvoiceLog>> GetAll(CancellationToken cancellationToken = default) =>
            await All()
                    .Include(l => l.Invoice)
                .ToListAsync(cancellationToken);

        public async Task<IList<InvoiceLog>> GetAllByInvoice(Invoice invoice, CancellationToken cancellationToken = default) =>
             await All()
                    .Include(l => l.Invoice)
                .Where(l => l.Invoice == invoice)
                .ToListAsync(cancellationToken);
    }
}
