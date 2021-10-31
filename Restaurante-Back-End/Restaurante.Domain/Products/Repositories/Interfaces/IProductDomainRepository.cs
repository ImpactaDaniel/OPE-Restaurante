using Restaurante.Domain.Common.Data.Models;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Products.Models;
using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Products.Repositories.Interfaces
{
    public interface IProductDomainRepository : IDomainRepository<Product>
    {
        Task<bool> Update(Product entity, CancellationToken cancellationToken = default);
        Task<bool> Delete(Product entity, CancellationToken cancellationToken = default);
        Task<PaginationInfo<Product>> GetAll(int start, int length, CancellationToken cancellationToken = default);
        Task<PaginationInfo<Product>> Search(Expression<Func<Product, bool>> condition, int page, int limit, CancellationToken cancellationToken = default);
    }
}
