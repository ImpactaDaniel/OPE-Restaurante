using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Customers.Models;
using System;
using System.Collections.Generic;

namespace Restaurante.Domain.Baskets.Models
{
    public class Basket : Entity<int>
    {
        public Customer Customer { get; set; }
        public ICollection<BasketItem> Items { get; set; } = new List<BasketItem>();
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
        public int CustomerId { get; set; }
    }
}
