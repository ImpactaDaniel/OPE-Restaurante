using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Restaurante.Application.Hubs;
using Restaurante.Application.Hubs.Interfaces;
using Restaurante.Application.Integrations.Events;
using Restaurante.Application.Invoices.Common.Models.Enums;
using Restaurante.Domain.Integrations.Interface;
using Restaurante.Domain.Invoices.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Notifications
{
    public class InvoiceUpdatedNotification : INotification
    {
        public Invoice Invoice { get; set; }
        public InvoiceNotificationType NotificationType { get; set; }
        internal class InvoiceUpdatedNotificationHandler : INotificationHandler<InvoiceUpdatedNotification>
        {
            private readonly ILogger<InvoiceUpdatedNotificationHandler> _logger;
            private readonly IEventBus _eventBus;
            public InvoiceUpdatedNotificationHandler(ILogger<InvoiceUpdatedNotificationHandler> logger, IEventBus eventBus)
            {
                _logger = logger;
                _eventBus = eventBus;
            }

            public async Task Handle(InvoiceUpdatedNotification notification, CancellationToken cancellationToken)
            {
                try
                {
                    //chamar service bus para publicar a mensagem
                    var message = new InvoiceUpdatedIntegrationEvent
                    {
                        Payload = notification.Invoice
                    };
                    await _eventBus.PublishAsync("InvoiceQueue", message, cancellationToken);
                }
                catch (Exception e)
                {
                    _logger.LogError(e, e.Message);
                }
            }
        }
    }
}
