using MediatR;
using Restaurante.Application.BasicEntities.Common;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.BasicEntities.Requests.Common
{
    public class CreateEntityRequest<TRequest, TEntity> : BasicEntityRequest<TEntity, CreateEntityRequest<TRequest, TEntity>, int>, IRequest<Response<bool>>
        where TEntity : class, IBasicEntity
    {
        public TRequest EntityRequest { get; set; }
    }

    public class GetEntityRequest<TEntity> : BasicEntityRequest<TEntity, GetEntityRequest<TEntity>, int>, IRequest<Response<TEntity>>
        where TEntity : class, IBasicEntity
    {
    }

    public class GetAllEntitiesRequest<TEntity> : BasicEntityRequest<TEntity, GetAllEntitiesRequest<TEntity>, int>, IRequest<Response<PaginationInfo<TEntity>>>
        where TEntity : class, IBasicEntity
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }

    public class SearchEntitiesRequest<TEntity> : BasicEntityRequest<TEntity, SearchEntitiesRequest<TEntity>, int>, IRequest<Response<PaginationInfo<TEntity>>>
        where TEntity : class, IBasicEntity
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public string Field { get; set; }
        public string Value { get; set; }
    }

    internal abstract class BasicEntityRequestsHandler<TRequest, TEntity> :
        IRequestHandler<CreateEntityRequest<TRequest, TEntity>, Response<bool>>,
        IRequestHandler<GetAllEntitiesRequest<TEntity>, Response<PaginationInfo<TEntity>>>,
        IRequestHandler<GetEntityRequest<TEntity>, Response<TEntity>>,
        IRequestHandler<SearchEntitiesRequest<TEntity>, Response<PaginationInfo<TEntity>>>
        where TEntity : class, IBasicEntity
    {
        protected readonly IBasicEntitiesService _basicEntitiesService;
        protected readonly INotifier _notifier;

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

        public async Task<Response<PaginationInfo<TEntity>>> Handle(GetAllEntitiesRequest<TEntity> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.GetEntities<TEntity>(request.Limit * request.Page, request.Limit, cancellationToken);
                return new Response<PaginationInfo<TEntity>>(true, res);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<PaginationInfo<TEntity>>(false, null);
            }
        }

        public abstract Task<Response<TEntity>> Handle(GetEntityRequest<TEntity> request, CancellationToken cancellationToken);
        public abstract Task<Response<PaginationInfo<TEntity>>> Handle(SearchEntitiesRequest<TEntity> request, CancellationToken cancellationToken);
    }
}
