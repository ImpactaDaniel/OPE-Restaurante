using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Invoices.Models.Enum;
using Restaurante.Domain.Users.Customers.Models;
using System;

namespace Restaurante.Domain.Invoices.Models
{
    public class Payment : Entity<int>
    {
        public DateTime PaymentTime { get; set; }
        public Customer Customer { get; set; }
        public decimal Amount { get; set; }
        public PaymentType PaymentType { get; set; }
    }
}
