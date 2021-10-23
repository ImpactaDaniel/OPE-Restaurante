using Restaurante.Domain.Invoices.Models;
using System;

namespace Restaurante.Application.Invoices.Common.Models
{
    public class InvoiceCreatedEventArgs : EventArgs
    {
        public Invoice Invoice { get; set; }
    }
}
