using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Products.Services.Interfaces
{
    public interface IProductService : IEntityService<Product>
    {
        Task<IEnumerable<Product>> GetAll(CancellationToken cancellationToken = default);
        Task<bool> CreateProduct(Product product, int currentUserId, CancellationToken cancellationToken = default);
        Task<bool> UpdateProduct(int id, Product productm, int currentUserId, CancellationToken cancellationToken = default);
    }
}
