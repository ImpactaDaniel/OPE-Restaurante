using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Common.Models.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
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
    }
}
