using Restaurante.Domain.Common.Models.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Repositories.Interfaces
{
    public interface IDomainRepository<TEntity> where TEntity : IEntity
    {
        Task<TEntity> Save(TEntity entidade, CancellationToken cancellationToken = default);
        TEntity Get(Func<TEntity, bool> condicao);
    }
}
