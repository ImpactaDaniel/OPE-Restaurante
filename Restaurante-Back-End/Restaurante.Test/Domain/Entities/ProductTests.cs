using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Products.Models;
using System;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class ProductTests
    {

        private static Product NewProduct()
        {
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photoPath = "C:\\Test\\photo";

            return new Product(name, description, price, quantity, category, photoPath, accompaniments, true);
        }

        [Fact]
        public void ShouldCreateNewProductWithPrice()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var available = true;

            //act
            var product = new Product(name, description, price, available);

            //assert
            Assert.NotNull(product);
            Assert.Equal(name, product.Name);
            Assert.Equal(description, product.Description);
            Assert.Equal(price, product.Price);
            Assert.Equal(available, product.Available);
        }

        [Fact]
        public void ShouldCreateNewProductWithQuantity()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;

            //act
            var product = new Product(name, description, price, quantity);

            //assert
            Assert.NotNull(product);
            Assert.Equal(quantity, product.QuantityStock);
        }

        [Fact]
        public void ShouldCreateNewProductWithCategory()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");

            //act
            var product = new Product(name, description, price, quantity, category);

            //assert
            Assert.NotNull(product);
            Assert.Equal(category.Name, product.Category.Name);
        }

        [Fact]
        public void ShouldNotCreateNewProductWithCategoryNull()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            Product product = null;

            Assert.Throws<BasicTableException>(() =>
                //act
                product = new Product(name, description, price, quantity, null)
            );
            //assert
            Assert.Null(product);
        }

        [Fact]
        public void ShouldCreateNewProductWithAccompaniments()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";

            //act
            var product = new Product(name, description, price, quantity, category, accompaniments);

            //assert
            Assert.NotNull(product);
            Assert.Equal(accompaniments, product.Accompaniments);
        }

        [Fact]
        public void ShouldCreateNewProductWithPhoto()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photo = new Photo("C:\\Test\\photo");

            //act
            var product = new Product(name, description, price, quantity, category, photo, accompaniments);

            //assert
            Assert.NotNull(product);
            Assert.Equal(photo.Path, product.Photo.Path);
        }

        [Fact]
        public void ShouldNotCreateNewProductWithPhotoNull()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            Photo photo = null;
            Product product = null;

            Assert.Throws<BasicTableException>(() =>
                //act
                product = new Product(name, description, price, quantity, category, photo, accompaniments)
            );
            //assert
            Assert.Null(product);
        }

        [Fact]
        public void ShouldCreateNewProductWithPhotoPath()
        {
            //arrange
            string name = "Test Product", description = "Test Product Description";
            var price = 10.99m;
            var quantity = 155;
            var category = new ProductCategory("Frango");
            var accompaniments = "Test accompaniment";
            var photoPath = "C:\\Test\\photo";

            //act
            var product = new Product(name, description, price, quantity, category, photoPath, accompaniments);

            //assert
            Assert.NotNull(product);
            Assert.Equal(photoPath, product.Photo.Path);
        }

        [Fact]
        public void ShouldUpdateProductName()
        {
            //arrange
            var product = NewProduct();
            var updatedProductName = "test product update";
            var updatedTime = DateTime.Now;

            //act
            product.UpdateName(updatedProductName);

            //assert
            Assert.Equal(updatedProductName, product.Name);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldUpdateProductDescription()
        {
            //arrange
            var product = NewProduct();
            var updatedProductDescription = "test product update description";
            var updatedTime = DateTime.Now;

            //act
            product.UpdateDescription(updatedProductDescription);

            //assert
            Assert.Equal(updatedProductDescription, product.Description);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldAddProductQuantity()
        {
            //arrange
            var product = NewProduct();
            var quantity = 10;
            var actualProductQuantity = product.QuantityStock;
            var updatedTime = DateTime.Now;

            //act
            product.AddQuantity(quantity);

            //assert
            Assert.Equal(quantity + actualProductQuantity, product.QuantityStock);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldRemoveProductQuantity()
        {
            //arrange
            var product = NewProduct();
            var quantity = 10;
            var actualProductQuantity = product.QuantityStock;
            var updatedTime = DateTime.Now;

            //act
            product.RemoveQuantity(quantity);

            //assert
            Assert.Equal(actualProductQuantity - quantity, product.QuantityStock);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldUpdateProductPhoto()
        {
            //arrange
            var product = NewProduct();
            var newPhoto = new Photo("C:\\TesteUpdate\\photoPathTestUpdate");
            var updatedTime = DateTime.Now;

            //act
            product.UpdatePhoto(newPhoto);

            //assert
            Assert.Equal(newPhoto.Path, product.Photo.Path);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldNotUpdateProductPhoto()
        {
            //arrange
            var product = NewProduct();
            var photoExpected = product.Photo;
            Photo photo = null;

            //assert
            Assert.Throws<BasicTableException>(() =>
                 //act
                 product.UpdatePhoto(photo)
            );

            Assert.Equal(photoExpected.Path, product.Photo.Path);
            Assert.Null(product.UpdatedOn);
        }

        [Fact]
        public void ShouldUpdateProductPhotoPath()
        {
            //arrange
            var product = NewProduct();
            var newPhoto = "C:\\TesteUpdate\\photoPathTestUpdate";
            var updatedTime = DateTime.Now;

            //act
            product.UpdatePhoto(newPhoto);

            //assert
            Assert.Equal(newPhoto, product.Photo.Path);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldUpdateProductPrice()
        {
            //arrange
            var product = NewProduct();
            var newPrice = 99.99m;
            var updatedTime = DateTime.Now;

            //act
            product.UpdatePrice(newPrice);

            //assert
            Assert.Equal(newPrice, product.Price);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldUpdateProductAvailable()
        {
            //arrange
            var product = NewProduct();
            var available = false;
            var updatedTime = DateTime.Now;

            //act
            product.UpdateAvailable(available);

            //assert
            Assert.Equal(available, product.Available);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldUpdateProductCategory()
        {
            //arrange
            var product = NewProduct();
            var category = new ProductCategory("test update");
            var updatedTime = DateTime.Now;

            //act
            product.UpdateCategory(category);

            //assert
            Assert.Equal(category.Name, product.Category.Name);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }

        [Fact]
        public void ShouldNotUpdateProductCategory()
        {
            //arrange
            var product = NewProduct();
            var expectedCategory = product.Category;

            //assert
            Assert.Throws<BasicTableException>(() =>
                //act
                product.UpdateCategory(null)
            );

            Assert.Equal(expectedCategory.Name, product.Category.Name);
            Assert.Null(product.UpdatedOn);
        }

        [Fact]
        public void ShouldUpdateProductAccompaniments()
        {
            //arrange
            var product = NewProduct();
            var updatedTime = DateTime.Now;
            var accompanimentsExpected = "Test update accompaniments";

            //act
            product.UpdateAccompaniments(accompanimentsExpected);

            //assert
            Assert.Equal(accompanimentsExpected, product.Accompaniments);
            Assert.Equal(updatedTime.ToShortDateString(), product.UpdatedOn.Value.ToShortDateString());
        }
    }
}
