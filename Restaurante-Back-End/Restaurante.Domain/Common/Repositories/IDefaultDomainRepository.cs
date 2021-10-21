using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Repositories.Interfaces
{
    public interface IDefaultDomainRepository
    {
        Task<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> condicao, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task<PaginationInfo<TEntity>> Search<TEntity>(Expression<Func<TEntity, bool>> condicao, int start, int limit, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task<TEntity> Create<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task<IEnumerable<TEntity>> GetAll<TEntity>(CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task<PaginationInfo<TEntity>> GetAll<TEntity>(int start, int limit, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
        Task<bool> Delete<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
    }
}
