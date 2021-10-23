using Restaurante.Domain.Invoices.Models;
using System;

namespace Restaurante.Application.Invoices.Common.Models
{
    public class InvoiceEventArgs : EventArgs
    {
        public Invoice Invoice { get; set; }
    }
}
