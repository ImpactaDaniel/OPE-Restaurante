using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Products
{
    public class ProductRepository : DataRepository<IRestauranteDbContext, Product>, IProductDomainRepository
    {
        public ProductRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task<bool> Delete(Product product, CancellationToken cancellationToken = default)
        {
            Data.Products.Remove(product);
            return await Data.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<IEnumerable<Product>> GetAll(int start, int length, CancellationToken cancellationToken = default)
        {
            return await All()
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .Skip(start)
                .Take(length)
                .ToListAsync(cancellationToken);
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

        public async Task<IEnumerable<Product>> Search(string name, CancellationToken cancellationToken = default)
        {
            return await All()
                .Where(p => EF.Functions.Like(p.Name, $"%{name}%"))
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }
    }
}
