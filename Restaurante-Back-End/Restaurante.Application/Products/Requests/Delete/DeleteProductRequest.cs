using MediatR;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Products.Common.Models;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using Restaurante.Domain.Products.Factories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Products.Requests.Delete
{
    public class DeleteProductRequest : ProductRequest<DeleteProductRequest>, IRequest<Response<bool>>
    {
        internal class DeleteProductRequestHandler : IRequestHandler<DeleteProductRequest, Response<bool>>
        {
            private readonly IProductService _service;
            private readonly IInvoiceDomainRepository _invoiceRepository;
            private readonly INotifier _notifier;

            public DeleteProductRequestHandler(
                IProductService service,
                INotifier notifier,
                IInvoiceDomainRepository invoiceDomainRepository)
            {
                _service = service;
                _notifier = notifier;
                _invoiceRepository = invoiceDomainRepository;
            }

            public async Task<Response<bool>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var invoice = await _invoiceRepository.Get(i => i.Products.Any(i => i.Product.Id == request.Id) && (i.Status != Domain.Invoices.Models.Enum.InvoiceStatus.Closed && i.Status != Domain.Invoices.Models.Enum.InvoiceStatus.Rejected), cancellationToken);

                    if (invoice is not null)
                        throw new BasicTableException("Produto está presente em um pedido ativo!", Domain.Common.Enums.NotificationKeys.Error);

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
