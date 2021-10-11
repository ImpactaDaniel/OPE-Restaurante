using NSubstitute;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class ProductServiceTests
    {
        private readonly IProductService _service;
        private readonly IProductDomainRepository _repository;

        public ProductServiceTests()
        {
            _repository = Substitute.For<IProductDomainRepository>();
        }

        [Fact]
        public async Task ShouldCreateNewProduct()
        {
            //arrange
            _repository.Save(Arg.Any<Product>()).ReturnsForAnyArgs(true);
            var product = new Product("test", "test", 10m, true);

            //act
            await _service.CreateProduct(product);

            //assert
            await _repository.ReceivedWithAnyArgs().Save(Arg.Any<Product>());
        }
    }
}
