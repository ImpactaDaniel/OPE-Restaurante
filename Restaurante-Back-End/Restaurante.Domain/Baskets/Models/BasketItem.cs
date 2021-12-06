using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Products.Models;

namespace Restaurante.Domain.Baskets.Models
{
    public class BasketItem : Entity<int>
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Obs { get; set; }
    }
}
