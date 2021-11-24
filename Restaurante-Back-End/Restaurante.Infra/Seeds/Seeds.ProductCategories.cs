using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
using System.Collections.Generic;

namespace Restaurante.Infra.Seeds
{
    internal static partial class Seeds
    {
        public static void ProductCategoriesSeed(this ModelBuilder builder)
        {
            var productCategories = new List<ProductCategory>
            {
                new ProductCategory(1, "Sobremensa"),
                new ProductCategory(2, "Acompanhamento"),
                new ProductCategory(3, "Prato Principal"),
                new ProductCategory(4, "Bebidas")
            };

            builder.Entity<ProductCategory>().HasData(productCategories);
        }
    }
}
