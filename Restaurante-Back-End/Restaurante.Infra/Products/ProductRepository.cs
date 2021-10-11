using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Products
{
    public class ProductRepository : DataRepository<IRestauranteDbContext, Product>, IProductDomainRepository
    {
        public ProductRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default)
        {
            return await All()
                .Include(p => p.Photo)
                .Include(p => p.Category)
                .ToListAsync(cancellationToken);
        }

        public Task<bool> Update(int id, Product entity, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public override async Task<Product> Get(Expression<Func<Product, bool>> condicao, CancellationToken cancellationToken = default)
        {
            var entity = await All()
                                .Include(p => p.Photo)
                                .Include(p => p.Category)
                                .FirstOrDefaultAsync(condicao, cancellationToken);
            return entity;
        }
    }
}
