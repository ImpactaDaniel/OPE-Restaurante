using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Products.Models
{
    public class ProductCategory : Entity<int>
    {
        public string Name { get; set; }
    }
}
