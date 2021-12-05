using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Baskets.Models;
using Restaurante.Domain.Baskets.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Baskets.Repositories
{
    public class BasketRepository : DataRepository<IRestauranteDbContext, Basket>, IBasketRepository
    {
        public BasketRepository(IRestauranteDbContext db) : base(db)
        {
        }

        public async Task AddOrUpdateItem(int customerId, BasketItem item, CancellationToken cancellationToken = default)
        {
            var basket = await GetOrCreateBasket(customerId, cancellationToken);

            basket.Items.Add(item);

            Data.Baskets.Attach(basket);

            await Data.SaveChangesAsync(cancellationToken);
        }

        public async Task<Basket> GetActiveBasket(int customerId, CancellationToken cancellationToken = default) =>
            await All()
                .AsNoTracking()
                .FirstOrDefaultAsync(b => 
                                b.Active &&
                                b.CustomerId == customerId, cancellationToken);

        public async Task<Basket> GetOrCreateBasket(int customerId, CancellationToken cancellationToken = default)
        {
            var currentBasket = await GetActiveBasket(customerId, cancellationToken);

            if (currentBasket == null)
                currentBasket = new Basket();

            return currentBasket;
        }
    }
}
