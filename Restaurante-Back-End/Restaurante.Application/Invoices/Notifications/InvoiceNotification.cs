using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Hubs;
using Restaurante.Application.Hubs.Interfaces;
using Restaurante.Application.Invoices.Common.Models.Enums;
using Restaurante.Domain.Invoices.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Notifications
{
    public class InvoiceNotification : INotification
    {
        public Invoice Invoice { get; set; }
        public InvoiceNotificationType NotificationType { get; set; }
        internal class InvoiceNotificationHandler : INotificationHandler<InvoiceNotification>
        {
            private readonly IHubContext<InvoiceHub, IInvoiceNotifier> _hub;
            private readonly ILogger<InvoiceNotificationHandler> _logger;
            public InvoiceNotificationHandler(IHubContext<InvoiceHub, IInvoiceNotifier> hub, ILogger<InvoiceNotificationHandler> logger)
            {
                _hub = hub;
                _logger = logger;
            }

            public async Task Handle(InvoiceNotification notification, CancellationToken cancellationToken)
            {
                try
                {
                    await _hub.Clients.All.NewInvoiceNotification(notification.Invoice, notification.NotificationType, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }
        }
    }
}
