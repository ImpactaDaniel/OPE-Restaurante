using Restaurante.Application.Products.Factories;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Products.Factories.Interfaces;
using Restaurante.Domain.Products.Models;
using Xunit;

namespace Restaurante.Test.Domain.Factories
{
    public class ProductFactoryTests
    {
        private readonly IProductFactory _factory;

        public ProductFactoryTests()
        {
            _factory = new ProductFactory();
        }

        [Fact]
        public void ShouldCreateNewFullProduct()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photoPath = "C:\\Test\\photo";

            //act
            var product = _factory
                            .WithName(name)
                            .WithDescription(description)
                            .WithPrice(price)
                            .WithQuantity(quantity)
                            .WithCategory(category)
                            .WithAccompaniments(accompaniments)
                            .WithPhoto(photoPath)
                            .Build();

            //assert
            Assert.NotNull(product);
            Assert.Equal(price, product.Price);
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(category.Name, product.Category.Name);
            Assert.Equal(accompaniments, product.Accompaniments);
            Assert.Equal(photoPath, product.Photo.Path);
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsNotSetted()
        {
            //arrange
            string description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photoPath = "C:\\Test\\photo";

            //assert
            Assert.Throws<BasicTableException>(() =>
                        _factory
                            .WithDescription(description)
                            .WithPrice(price)
                            .WithQuantity(quantity)
                            .WithCategory(category)
                            .WithAccompaniments(accompaniments)
                            .WithPhoto(photoPath)
                            .Build()
            );
        }

        [Fact]
        public void ShouldThrowExceptionWhenDescriptionIsNotSetted()
        {
            //arrange
            string name = "Test Product";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photoPath = "C:\\Test\\photo";

            //assert
            Assert.Throws<BasicTableException>(() =>
                        _factory
                            .WithName(name)
                            .WithPrice(price)
                            .WithQuantity(quantity)
                            .WithCategory(category)
                            .WithAccompaniments(accompaniments)
                            .WithPhoto(photoPath)
                            .Build()
            );
        }

        [Fact]
        public void ShouldThrowExceptionWhenPriceIsNotSetted()
        {
            //arrange
            string name = "test", description = "Test Product Description";
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photoPath = "C:\\Test\\photo";

            //assert
            Assert.Throws<BasicTableException>(() =>
                        _factory
                            .WithName(name)
                            .WithDescription(description)
                            .WithQuantity(quantity)
                            .WithCategory(category)
                            .WithAccompaniments(accompaniments)
                            .WithPhoto(photoPath)
                            .Build()
            );
        }
    }
}
