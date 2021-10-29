using MediatR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Common;
using Restaurante.Application.Common.Helper;
using Restaurante.Application.Invoices.Common.Models;
using Restaurante.Application.Invoices.Notifications;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Common.Exceptions;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Invoices.Models;
using Restaurante.Domain.Invoices.Models.Enum;
using Restaurante.Domain.Invoices.Repositories.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Requests.Update
{
    public class UpdateInvoiceStatusRequest : InvoiceRequest<UpdateInvoiceStatusRequest>, IRequest<Response<Invoice>>
    {
        public InvoiceStatus Status { get; set; }

        internal class UpdateInvoiceStatusRequestHandler : IRequestHandler<UpdateInvoiceStatusRequest, Response<Invoice>>
        {
            private readonly INotifier _notifier;
            private readonly IInvoiceDomainRepository _invoiceRespository;
            private readonly ILogger<UpdateInvoiceStatusRequestHandler> _logger;
            private readonly IMediator _mediator;
            private readonly IInvoiceLogDomainRepository _logRepository;

            public UpdateInvoiceStatusRequestHandler(INotifier notifier, IInvoiceDomainRepository invoiceRespository, ILogger<UpdateInvoiceStatusRequestHandler> logger, IMediator mediator, IInvoiceLogDomainRepository logRepository)
            {
                _notifier = notifier;
                _invoiceRespository = invoiceRespository;
                _logger = logger;
                _mediator = mediator;
                _logRepository = logRepository;
            }

            public async Task<Response<Invoice>> Handle(UpdateInvoiceStatusRequest request, CancellationToken cancellationToken)
            {
                try
                {
                    var invoice = await _invoiceRespository.Get(i => i.Id == request.Id, cancellationToken);

                    if (invoice is null)
                        throw new BasicTableException("Pedido não encontrado!", Domain.Common.Enums.NotificationKeys.EntityNotFound);

                    invoice.Status = request.Status;

                    var updated = await _invoiceRespository.Save(invoice, cancellationToken);

                    if (updated)
                    {
                        await _logRepository.CreateLog(new InvoiceLog
                        {
                            Date = DateTime.Now,
                            Invoice = invoice,
                            Message = $"Pedido {invoice.Id} atualizado.",
                            Type = InvoiceLogType.Updated
                        }, cancellationToken);

                        await _mediator.Publish(new InvoiceNotification
                        {
                            Invoice = invoice,
                            NotificationType = Common.Models.Enums.InvoiceNotificationType.Updated
                        }, cancellationToken);
                    }

                    return new Response<Invoice>(updated, invoice);

                }
                catch (RestauranteException e)
                {
                    _notifier.AddNotification(NotificationHelper.FromException(e));
                    return new Response<Invoice>(false, null);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                    throw new Exception($"Houve um erro ao atualizar o status do pedido {request.Id}");
                }
            }
        }
    }
}
