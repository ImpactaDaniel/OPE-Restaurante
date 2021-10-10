using Restaurante.Domain.Products.Models;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class ProductCategoryTests
    {
        [Fact]
        public void ShouldCreateNewProductCategoryWithExpectedName()
        {
            //arrange
            var expectedName = "Test create new product category";

            //act
            var category = new ProductCategory(expectedName);

            //assert
            Assert.Equal(expectedName, category.Name);
        }

        [Fact]
        public void ShouldUpdateProductCategory()
        {
            //arrange
            var expectedName = "Test update product category";
            var category = new ProductCategory("test");

            //act
            category.UpdateName(expectedName);

            //assert
            Assert.Equal(expectedName, category.Name);

        }
    }
}
