using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Restaurante.Domain.Common.Data.Models;

namespace Restaurante.Infra.Products
{
    public class ProductRepository : DataRepository<IRestauranteDbContext, Product>, IProductDomainRepository
    {
        public ProductRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<bool> Delete(Product product, CancellationToken cancellationToken = default)
        {
            await DeleteInvoiceLines(product, cancellationToken);
            Data.Products.Remove(product);
            return await Data.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<PaginationInfo<Product>> GetAll(int start, int length, CancellationToken cancellationToken = default)
        {
            var entities = await All()
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .Skip(start)
                .Take(length)
                .ToListAsync(cancellationToken);

            var count = await All()
                .CountAsync(cancellationToken);

            return new PaginationInfo<Product>
            {
                Entities = entities,
                Size = count
            };
        }

        public async Task<bool> Update(Product entity, CancellationToken cancellationToken = default)
        {
            Data.Products.Update(entity);
            return await Data.SaveChangesAsync(cancellationToken) > 0;
        }

        public override async Task<Product> Get(Expression<Func<Product, bool>> condicao, CancellationToken cancellationToken = default)
        {
            var entity = await All()
                                .Include(p => p.Photo)
                                .Include(p => p.Category)
                                .FirstOrDefaultAsync(condicao, cancellationToken);
            return entity;
        }

        public async Task<PaginationInfo<Product>> Search(Expression<Func<Product, bool>> condition, int page, int limit, CancellationToken cancellationToken = default)
        {
            var entities = await All()
                .Where(condition)
                .Skip(page * limit)
                .Take(limit)
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);

            var count = await All()
                        .Where(condition)
                        .CountAsync(cancellationToken);

            return new PaginationInfo<Product>
            {
                Entities = entities,
                Size = count
            };
        }

        private async Task DeleteInvoiceLines(Product product, CancellationToken cancellationToken = default)
        {
            var invoiceLines = await Data.InvoiceLines.Where(il => il.Product.Id == product.Id).ToListAsync(cancellationToken);

            foreach (var invoiceLine in invoiceLines)
                Data.InvoiceLines.Remove(invoiceLine);

            await Data.SaveChangesAsync(cancellationToken);
        }
    }
}
