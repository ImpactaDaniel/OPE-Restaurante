using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Products.Services.Interfaces
{
    public interface IProductService : IEntityService<Product>
    {
        Task<PaginationInfo<Product>> GetAll(int page, int limit = 20, CancellationToken cancellationToken = default);
        Task<bool> CreateProduct(Product product, int currentUserId, CancellationToken cancellationToken = default);
        Task<bool> UpdateProduct(int id, Product product, int currentUserId, CancellationToken cancellationToken = default);
        Task<PaginationInfo<Product>> SearchProducts(Expression<Func<Product, bool>> condition, int page, int limit, CancellationToken cancellationToken = default);
        Task<bool> DeleteProduct(int id, int currentUserId, CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductCategory>> GetProductsGroupByCategories(CancellationToken cancellationToken = default);
    }
}
