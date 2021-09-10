﻿using Restaurante.Domain.Common.Models.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Repositories.Interfaces
{
    public interface IDefaultDomainRepository
    {
        Task<TEntity> Get<TEntity>(Expression<Func<TEntity, bool>> condicao, CancellationToken cancellationToken = default) where TEntity : class, IEntity;
    }
}
