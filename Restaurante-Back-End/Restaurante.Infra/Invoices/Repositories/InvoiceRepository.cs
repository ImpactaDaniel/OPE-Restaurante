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
using System.Linq.Expressions;
using System;
using Restaurante.Domain.Common.Data.Models;

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

        public override async Task<Invoice> Get(Expression<Func<Invoice, bool>> condicao, CancellationToken cancellationToken = default) =>
            await All()
                    .AsNoTrackingWithIdentityResolution()
                    .Include(i => i.Products)
                        .ThenInclude(p => p.Product)
                    .Include(i => i.Address)
                    .Include(i => i.Customer)
                    .Include(i => i.Payment)
                  .FirstOrDefaultAsync(condicao, cancellationToken);

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

        public async Task<IList<Invoice>> GetAll(Expression<Func<Invoice, bool>> condition, CancellationToken cancellationToken = default) =>
             await All()
                        .Include(i => i.Products)
                            .ThenInclude(p => p.Product)
                        .Include(i => i.Address)
                        .Include(i => i.Customer)
                        .Include(i => i.Payment)
                    .Where(condition)
                    .ToListAsync(cancellationToken);

        public async Task<PaginationInfo<Invoice>> GetAll(int page, int limit, CancellationToken cancellationToken = default)
        {
            var invoices = await All()
                        .Include(i => i.Products)
                            .ThenInclude(p => p.Product)
                        .Include(i => i.Address)
                        .Include(i => i.Customer)
                        .Include(i => i.Payment)
                    .Skip(page * limit)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

            var count = await All()
                        .Include(i => i.Products)
                            .ThenInclude(p => p.Product)
                        .Include(i => i.Address)
                        .Include(i => i.Customer)
                        .Include(i => i.Payment)
                    .CountAsync(cancellationToken);

            return new PaginationInfo<Invoice>
            {
                Entities = invoices,
                Size = count
            };
        }

        public async Task<PaginationInfo<Invoice>> GetAll(Expression<Func<Invoice, bool>> condition, int page, int limit, CancellationToken cancellationToken = default)
        {
            var invoices = await All()
                        .Include(i => i.Products)
                            .ThenInclude(p => p.Product)
                        .Include(i => i.Address)
                        .Include(i => i.Customer)
                        .Include(i => i.Payment)
                    .Where(condition)
                    .Skip(page * limit)
                    .Take(limit)
                    .ToListAsync(cancellationToken);

            var count = await All()
                        .Include(i => i.Products)
                            .ThenInclude(p => p.Product)
                        .Include(i => i.Address)
                        .Include(i => i.Customer)
                        .Include(i => i.Payment)
                    .Where(condition)
                    .CountAsync(cancellationToken);

            return new PaginationInfo<Invoice>
            {
                Entities = invoices,
                Size = count
            };
        }
    }
}
