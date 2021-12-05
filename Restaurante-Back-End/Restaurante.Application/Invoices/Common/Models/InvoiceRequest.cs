using Restaurante.Application.Common;
using Restaurante.Domain.Invoices.Models.Enum;
using System.Collections.Generic;

namespace Restaurante.Application.Invoices.Common.Models
{
    public abstract class InvoiceRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public int BasketId { get; set; }
        public int CustomerId { get; set; }
        public int CustomerAddress { get; set; }
        public PaymentType PaymentType { get; set; }
    }

    public class ProductInvoiceRequest : EntityRequest<int>
    {
        public int Quantity { get; set; }
        public string Obs { get; set; }
    }
}
