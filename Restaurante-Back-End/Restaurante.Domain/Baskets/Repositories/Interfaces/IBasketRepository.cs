using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Baskets.Models;
using System.Threading.Tasks;
using System.Threading;

namespace Restaurante.Domain.Baskets.Repositories.Interfaces
{
    public interface IBasketRepository : IDomainRepository<Basket>
    {
        Task<Basket> GetActiveBasket(int customerId, CancellationToken cancellationToken = default);
        Task<Basket> GetOrCreateBasket(int customerId, CancellationToken cancellationToken = default);
        Task AddOrUpdateItem(int customerId, BasketItem item, CancellationToken cancellationToken = default);
        Task RemoveItem(int customerId, int productId, CancellationToken cancellationToken = default);
        Task InactiveBasket(int id, CancellationToken cancellationToken = default);
    }
}
