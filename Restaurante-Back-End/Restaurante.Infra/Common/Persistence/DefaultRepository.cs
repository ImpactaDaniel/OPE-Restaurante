using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Models.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Common.Persistence
{
    internal class DefaultRepository : IDefaultDomainRepository
    {
        private readonly IRestauranteDbContext _context;

        public DefaultRepository(IRestauranteDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> condicao, CancellationToken cancellationToken = default)
             where TEntity : class, IEntity
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(condicao, cancellationToken);
            return entity;
        }

        public async Task<TEntity> Create<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class, IEntity
        {
            await _context
                .Set<TEntity>()
                .AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }

        public async Task<IEnumerable<TEntity>> GetAll<TEntity>(CancellationToken cancellationToken)
            where TEntity : class, IEntity
        {
            return await _context
                .Set<TEntity>()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> Delete<TEntity>(TEntity entity, CancellationToken cancellationToken)
            where TEntity : class, IEntity
        {
            _context
                .Set<TEntity>()
                .Remove(entity);

            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<PaginationInfo<TEntity>> GetAll<TEntity>(int start, int limit, CancellationToken cancellationToken)
            where TEntity : class, IEntity
        {
            var list = await _context
                .Set<TEntity>()
                .Skip(start)
                .Take(limit)                
                .ToListAsync(cancellationToken);

            var count = await _context
                .Set<TEntity>()
                .CountAsync(cancellationToken);

            return new PaginationInfo<TEntity> { Entities = list, Size = count };
        }

        public async Task<PaginationInfo<TEntity>> Search<TEntity>(Expression<Func<TEntity, bool>> condicao, int start, int limit, CancellationToken cancellationToken)
            where TEntity : class, IEntity
        {
            var list = await _context
                .Set<TEntity>()
                .Where(condicao)
                .Skip(start)
                .Take(limit)
                .ToListAsync(cancellationToken);

            var count = await _context
                .Set<TEntity>()
                .Where(condicao)
                .CountAsync(cancellationToken);

            return new PaginationInfo<TEntity> { Entities = list, Size = count };
        }
    }
}
