using Restaurante.Domain.Common.Models.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Infra.Common.Helper;
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
        protected DataRepository(TDbContext db, INotifier notifier)
        {
            Data = db;
            Notifier = notifier;
        }
        protected TDbContext Data { get; }
        protected INotifier Notifier { get; }
        protected IQueryable<TEntity> All() => Data.Set<TEntity>();
        public TEntity Get(Func<TEntity, bool> condicao)
        {
            var entity = All().First(condicao);
            if (entity is null)
            {
                Notifier.AddNotification(NotificationHelper.EntityNotFound(nameof(TEntity)));
                return null;
            }
            return entity;
        }

        public async Task<TEntity> Save(TEntity entity, CancellationToken cancellationToken = default)
        {
            try
            {
                Data.Update(entity);
                await Data.SaveChangesAsync(cancellationToken);
                return entity;
            }catch(Exception e)
            {
                Notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }
    }
}
