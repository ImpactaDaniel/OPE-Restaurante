using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Products.Models;

namespace Restaurante.Domain.Invoices.Models
{
    public class InvoiceLine : Entity<int>
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public string Obs { get; set; }
    }
}
