using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Domain.Users.Customers.Models;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Infra.Invoices.Repositories
{
    public class InvoiceRepository : DataRepository<IRestauranteDbContext, Invoice>, IInvoiceDomainRepository
    {
        public InvoiceRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<Invoice> CreateInvoice(Invoice invoice, CancellationToken cancellationToken = default)
        {
            await Data.Invoices.AddAsync(invoice, cancellationToken);
            await Data.SaveChangesAsync(cancellationToken);
            return invoice;
        }

        public async Task<bool> Delete(Invoice invoice, CancellationToken cancellationToken = default)
        {
            Data.Invoices.Remove(invoice);
            return await Data.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IList<Invoice>> GetAll(CancellationToken cancellationToken = default) =>
            await All()
                    .AsNoTrackingWithIdentityResolution()
                    .Include(i => i.Products)
                        .ThenInclude(p => p.Product)
                    .Include(i => i.Address)
                    .Include(i => i.Customer)
                    .Include(i => i.Payment)
                  .ToListAsync(cancellationToken);

        public async Task<IList<Invoice>> GetAll(Customer customer, CancellationToken cancellationToken = default) =>
            await All()
                    .Include(i => i.Products)
                        .ThenInclude(p => p.Product)
                    .Include(i => i.Address)
                    .Include(i => i.Customer)
                    .Include(i => i.Payment)
                    .Where(i => i.Customer == customer)
                    .ToListAsync(cancellationToken);

    }
}
