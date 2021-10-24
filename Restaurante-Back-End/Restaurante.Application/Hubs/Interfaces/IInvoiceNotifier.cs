using Restaurante.Application.Invoices.Common.Models.Enums;
using Restaurante.Domain.Invoices.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Application.Hubs.Interfaces
{
    public interface IInvoiceNotifier
    {
        Task NewInvoiceNotification(Invoice invoice, InvoiceNotificationType notificationType, CancellationToken cancellationToken = default);
    }
}
