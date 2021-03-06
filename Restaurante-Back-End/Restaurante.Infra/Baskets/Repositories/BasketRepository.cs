using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Baskets.Models;
using Restaurante.Domain.Baskets.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
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

            basket = AddOrUpdateBasketItem(basket, item);

            Data.Baskets.Update(basket);

            await Data.SaveChangesAsync(cancellationToken);
        }

        public override Task<Basket> Get(Expression<System.Func<Basket, bool>> condicao, CancellationToken cancellationToken = default)
        {
            return All()
                .Include(b => b.Items)
                .FirstOrDefaultAsync(condicao, cancellationToken);
        }
        private static Basket AddOrUpdateBasketItem(Basket basket, BasketItem item)
        {
            var exists = basket.Items.Any(bi => bi.ProductId == item.ProductId);

            if (exists)
            {
                basket.Items.First(bi => bi.ProductId == item.ProductId).Quantity = item.Quantity;
                return basket;
            }

            basket.Items.Add(item);
            return basket;
        }

        private async Task<Basket> GetActiveBasketByCustomer(int customerId, CancellationToken cancellationToken = default) =>
            await All()
                    .Include(b => b.Items)
                    .FirstOrDefaultAsync(b => b.Active && b.CustomerId == customerId, cancellationToken);

        public async Task<Basket> GetActiveBasket(int customerId, CancellationToken cancellationToken = default) =>
            await All()
                .AsNoTracking()
                .Include(b => b.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Category)
                        .AsNoTracking()
                .Include(b => b.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Photo)
                .FirstOrDefaultAsync(b =>
                                b.Active &&
                                b.CustomerId == customerId, cancellationToken);

        public async Task<Basket> GetOrCreateBasket(int customerId, CancellationToken cancellationToken = default)
        {
            var currentBasket = await GetActiveBasketByCustomer(customerId, cancellationToken);

            if (currentBasket == null)
            {
                currentBasket = new Basket
                {
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CustomerId = customerId
                };
            }

            return currentBasket;
        }

        public async Task RemoveItem(int customerId, int productId, CancellationToken cancellationToken = default)
        {
            var basket = await GetOrCreateBasket(customerId, cancellationToken);

            var item = basket.Items.First(bi => bi.ProductId == productId);

            if (item != null)
                basket.Items.Remove(item);

            Data.Baskets.Attach(basket);

            await Data.SaveChangesAsync(cancellationToken);
        }

        public async Task InactiveBasket(int id, CancellationToken cancellationToken = default)
        {
            var basket = await All()
                .FirstAsync(b => b.Id == id, cancellationToken);

            basket.Active = false;

            Data.Baskets.Update(basket);

            await Data.SaveChangesAsync(cancellationToken);
        }
    }
}
