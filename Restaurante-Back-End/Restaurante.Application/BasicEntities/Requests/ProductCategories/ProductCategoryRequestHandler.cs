using Restaurante.Application.BasicEntities.Requests.Common;
using Restaurante.Application.BasicEntities.Requests.ProductCategories.Models;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.BasicEntities.Requests.ProductCategories
{
    internal class ProductCategoryRequestHandler : BasicEntityRequestsHandler<CreateProductCategoryRequest, ProductCategory>
    {
        public ProductCategoryRequestHandler(IBasicEntitiesService basicEntitiesService, INotifier notifier) : base(basicEntitiesService, notifier)
        {
        }

        public override async Task<Response<bool>> Handle(CreateEntityRequest<CreateProductCategoryRequest, ProductCategory> request, CancellationToken cancellationToken)
        {
            var newRequest = new CreateEntityRequest<CreateProductCategoryRequest, ProductCategory>
            {
                Entity = new ProductCategory(request.EntityRequest.Name)
            };
            return await base.Handle(newRequest, cancellationToken);
        }

        public override async Task<Response<ProductCategory>> Handle(GetEntityRequest<ProductCategory> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.GetEntity<ProductCategory>(category => category.Id == request.Id, cancellationToken);
                return new Response<ProductCategory>(true, res);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<ProductCategory>(false, null);
            }
        }

        public override async Task<Response<PaginationInfo<ProductCategory>>> Handle(SearchEntitiesRequest<ProductCategory> request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await _basicEntitiesService.GetEntities<ProductCategory>(category => category.Name.Contains(request.Value), request.Limit * request.Page, request.Limit, cancellationToken);
                return new Response<PaginationInfo<ProductCategory>>(true, res);
            }
            catch (BasicTableException e)
            {
                _notifier.AddNotification(NotificationHelper.FromException(e));
                return new Response<PaginationInfo<ProductCategory>>(false, null);
            }
        }
    }
}
