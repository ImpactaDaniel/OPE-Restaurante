using Microsoft.AspNetCore.SignalR;
using Restaurante.Application.Hubs.Interfaces;

namespace Restaurante.Application.Hubs
{
    public class InvoiceHub : Hub<IInvoiceNotifier>
    {
    }
}
