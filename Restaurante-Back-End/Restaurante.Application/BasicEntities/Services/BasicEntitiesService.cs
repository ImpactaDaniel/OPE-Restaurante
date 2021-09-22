using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Models.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.BasicEntities.Services
{
    internal class BasicEntitiesService : IBasicEntitiesService
    {
        private readonly IDefaultDomainRepository _basicEntityRepository;
        private readonly INotifier _notifier;

        public BasicEntitiesService(IDefaultDomainRepository basicEntityRepository, INotifier notifier)
        {
            _basicEntityRepository = basicEntityRepository;
            _notifier = notifier;
        }

        public async Task<IEntity> CreateEntity<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class, IBasicEntity
        {
            try
            {
                await _basicEntityRepository.Create(entity, cancellationToken);
                return entity;
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }

        public async Task<TEntity> GetEntity<TEntity>(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
            where TEntity : class, IBasicEntity
        {
            try
            {
                return await _basicEntityRepository.Get(expression, cancellationToken);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }

        public async Task<IEnumerable<TEntity>> GetEntities<TEntity>(CancellationToken cancellationToken)
            where TEntity : class, IBasicEntity
        {
            try
            {
                return await _basicEntityRepository.GetAll<TEntity>(cancellationToken);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return null;
            }
        }
    }
}
