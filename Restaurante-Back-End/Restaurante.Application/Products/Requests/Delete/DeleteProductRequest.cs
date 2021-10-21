using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Products.Common.Models;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Factories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Requests.Delete
{
    public class DeleteProductRequest : ProductRequest<DeleteProductRequest>, IRequest<Response<bool>>
    {
        internal class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Response<bool>>
        {
            private readonly IProductService _service;
            private readonly INotifier _notifier;

            public DeleteProductRequestHandler(
                IProductService service,
                INotifier notifier)
            {
                _service = service;
                _notifier = notifier;
            }

            public async Task<Response<bool>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var success = await _service.DeleteProduct(request.Id, request.CurrentUserId, cancellationToken);
                    return new Response<bool>(success, success);
                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<bool>(false, false);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}
