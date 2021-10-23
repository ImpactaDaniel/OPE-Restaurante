using System.Threading.Tasks;

namespace Restaurante.Application.Invoices.Common.Models.Delegates
{
    public delegate Task InvoiceCreatedEventHandler(object sender, InvoiceCreatedEventArgs e);
}
