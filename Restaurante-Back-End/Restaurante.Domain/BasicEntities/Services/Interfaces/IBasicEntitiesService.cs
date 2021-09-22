﻿using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.Common.Models.Interfaces;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.BasicEntities.Services.Interfaces
{
    public interface IBasicEntitiesService
    {
        Task<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
            where TEntity : class, IBasicEntity;
        Task<IEntity> CreateEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default) 
            where TEntity : class, IBasicEntity;
    }
}
