using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Invoices.Models.Enum;
using Restaurante.Domain.Users.Customers.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Invoices.Models
{
    public class Invoice : Entity<int>
    {
        public Customer Customer { get; set; }
        public IList<InvoiceLine> Products { get; set; }
        public InvoiceAddress Address { get; set; }
        public InvoiceStatus Status { get; set; }
        public Payment Payment { get; set; }
        public int CustomerId { get; set; }
        public IList<InvoiceLog> Logs { get; set; }
    }
}
