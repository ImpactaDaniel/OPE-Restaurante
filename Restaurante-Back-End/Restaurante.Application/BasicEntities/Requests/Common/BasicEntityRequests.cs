using MediatR;
using Restaurante.Application.BasicEntities.Common;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.BasicEntities.Requests.Common
{
    public class CreateEntityRequest<TRequest, TEntity> : BasicEntityRequest<TEntity, CreateEntityRequest<TRequest, TEntity>, int>, IRequest<Response<bool>>
        where TEntity : class, IBasicEntity
    {
        public TRequest EntityRequest { get; set; }
    }

    public class GetAllEntitiesRequest<TEntity> : BasicEntityRequest<TEntity, GetAllEntitiesRequest<TEntity>, int>, IRequest<Response<IEnumerable<TEntity>>>
        where TEntity : class, IBasicEntity
    {
    }
    internal abstract class BasicEntityRequestsHandler<TRequest, TEntity> :
        IRequestHandler<CreateEntityRequest<TRequest, TEntity>, Response<bool>>,
        IRequestHandler<GetAllEntitiesRequest<TEntity>, Response<IEnumerable<TEntity>>>
        where TEntity : class, IBasicEntity
    {
        private readonly IBasicEntitiesService _basicEntitiesService;
        private readonly INotifier _notifier;

        public BasicEntityRequestsHandler(IBasicEntitiesService basicEntitiesService, INotifier notifier)
        {
            _basicEntitiesService = basicEntitiesService;
            _notifier = notifier;
        }

        public virtual async Task<Response<bool>> Handle(CreateEntityRequest<TRequest, TEntity> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.CreateEntity(request.Entity, cancellationToken);
                return new Response<bool>(true, true);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<bool>(false, false);
            }
        }

        public async Task<Response<IEnumerable<TEntity>>> Handle(GetAllEntitiesRequest<TEntity> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.GetEntities<TEntity>(cancellationToken);
                return new Response<IEnumerable<TEntity>>(true, res);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<IEnumerable<TEntity>>(false, null);
            }
        }
    }
}
