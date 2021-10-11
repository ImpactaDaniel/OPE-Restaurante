using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Products.Models;

namespace Restaurante.Domain.Products.Factories.Interfaces
{
    public interface IProductFactory : IFactory<Product>
    {
        IProductFactory WithAvailability(bool available);
        IProductFactory WithQuantity(int quantity);
        IProductFactory WithPrice(decimal price);
        IProductFactory WithCategory(ProductCategory category);
        IProductFactory WithAccompaniments(string accompaniments);
        IProductFactory WithDescription(string description);
        IProductFactory WithName(string name);
        IProductFactory WithPhoto(Photo photo);
        IProductFactory WithPhoto(string photoPath);
    }
}
