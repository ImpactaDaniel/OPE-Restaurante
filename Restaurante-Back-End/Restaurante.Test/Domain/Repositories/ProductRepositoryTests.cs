using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Products;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Domain.Repositories
{
    public class ProductRepositoryTests
    {
        private readonly IProductDomainRepository _repository;
        private readonly IRestauranteDbContext _dbContext;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>()
                                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                .Options;

            _dbContext = new RestauranteDbContext(options);

            _repository = new ProductRepository(_dbContext);
        }

        [Fact]
        public async Task ShouldSaveNewProduct()
        {
            //arrange
            var product = new Product("test", "test", 10m, 10, new ProductCategory("Frango"), new Photo("Teste\\product"), "test", true);

            //act
            await _repository.Save(product);

            //assert
            var productSaved = await _repository.Get(p => p.Id == product.Id);

            Assert.NotNull(productSaved);
            Assert.Equal(productSaved.Name, product.Name);

        }
    }
}
