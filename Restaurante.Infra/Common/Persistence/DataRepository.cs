using Restaurante.Domain.Common.Models.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Common.Persistence
{
    public class DataRepository<TDbContext, TEntity> : IDomainRepository<TEntity>
        where TEntity : class, IEntity
        where TDbContext : IDbContext
    {
        protected DataRepository(TDbContext db)
        {
            Data = db;
        }
        protected TDbContext Data { get; }
        protected IQueryable<TEntity> All() => Data.Set<TEntity>();
        public TEntity Get(Func<TEntity, bool> condicao)
        {
            var entity = All().First(condicao);
            return entity;
        }

        public async Task<TEntity> Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            Data.Update(entity);
            await Data.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
