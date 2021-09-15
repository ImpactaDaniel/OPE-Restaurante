using Restaurante.Domain.Common.Models.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Repositories.Interfaces
{
    public interface IDomainRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> Save(TEntity entidade, CancellationToken cancellationToken = default);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> condicao, CancellationToken cancellationToken = default);
    }
}
