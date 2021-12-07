﻿using Microsoft.EntityFrameworkCore;
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

            Data.Products.Attach(item.Product);

            Data.Baskets.Attach(basket);

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
            var exists = basket.Items.Any(bi => bi.Product.Id == item.Product.Id);

            if (exists)
            {
                basket.Items.First(bi => bi.Id == item.Id).Quantity = item.Quantity;
                return basket;
            }

            basket.Items.Add(item);
            return basket;
        }

        public async Task<Basket> GetActiveBasket(int customerId, CancellationToken cancellationToken = default) =>
            await All()
                .AsNoTracking()
                .Include(b => b.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Category)
                .Include(b => b.Customer)
                    .ThenInclude(c => c.Addresses)
                .Include(b => b.Customer)
                    .ThenInclude(c => c.Phone)
                .Include(b => b.Items)
                    .ThenInclude(i => i.Product)
                        .ThenInclude(p => p.Photo)
                .FirstOrDefaultAsync(b =>
                                b.Active &&
                                b.CustomerId == customerId, cancellationToken);

        public async Task<Basket> GetOrCreateBasket(int customerId, CancellationToken cancellationToken = default)
        {
            var currentBasket = await GetActiveBasket(customerId, cancellationToken);

            if (currentBasket == null)
                currentBasket = new Basket
                {
                    Active = true,
                    CreatedDate = DateTime.Now,
                    CustomerId = customerId
                };

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
    }
}
