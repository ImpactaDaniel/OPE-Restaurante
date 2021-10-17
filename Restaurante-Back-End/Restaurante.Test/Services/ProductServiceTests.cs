using FizzWare.NBuilder;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Products.Services;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class ProductServiceTests
    {
        private readonly IProductService _service;
        private readonly IProductDomainRepository _repository;
        private readonly IEmployeeDomainRepository<Employee> _employeeDomainRepository;
        private readonly IBasicEntitiesService _basicEntitiesService;
        private readonly INotifier _notifier;

        public ProductServiceTests()
        {
            _repository = Substitute.For<IProductDomainRepository>();
            _employeeDomainRepository = Substitute.For<IEmployeeDomainRepository<Employee>>();
            _notifier = Substitute.For<INotifier>();
            _basicEntitiesService = Substitute.For<IBasicEntitiesService>();
            var logger = Substitute.For<ILogger<ProductService>>();

            _service = new ProductService(_repository, _notifier, logger, _employeeDomainRepository, _basicEntitiesService);
        }

        [Fact]
        public async Task ShouldCreateNewProduct()
        {
            //arrange
            _repository.Save(Arg.Any<Product>()).ReturnsForAnyArgs(true);
            _employeeDomainRepository.Get(Arg.Any<int>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());
            var product = new Product("test", "test", 10m, true);

            //act
            var success = await _service.CreateProduct(product, 1);

            //assert
            await _repository.ReceivedWithAnyArgs(1).Save(Arg.Any<Product>());
            await _employeeDomainRepository.ReceivedWithAnyArgs(1).Get(Arg.Any<int>());
            Assert.True(success);

        }

        [Fact]
        public async Task ShouldNotCreateNewProductWhenUserIsNull()
        {
            //arrange
            _repository.Save(Arg.Any<Product>()).ReturnsForAnyArgs(true);
            Employee employee = null;
            _employeeDomainRepository.Get(Arg.Any<int>()).ReturnsForAnyArgs(employee);
            var product = new Product("test", "test", 10m, true);

            //act
            var success = await _service.CreateProduct(product, 1);

            //assert
            await _repository.DidNotReceiveWithAnyArgs().Save(Arg.Any<Product>());
            await _employeeDomainRepository.ReceivedWithAnyArgs(1).Get(Arg.Any<int>());
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
            Assert.False(success);

        }

        [Fact]
        public async Task ShouldThrowExceptionWhenExceptionIsThrown()
        {
            //arrange
            _repository.WhenForAnyArgs(p => p.Save(Arg.Any<Product>())).Throw(new Exception());
            _employeeDomainRepository.Get(Arg.Any<int>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());
            var product = new Product("test", "test", 10m, true);

            //assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                //act
                _service.CreateProduct(product, 1)
            );

            Assert.Equal("Houve um erro ao tentar criar o produto!", ex.Message);
        }

        [Fact]
        public async Task ShouldReturnAllProductsFromRepository()
        {
            //arrange
            var productExpected = new Product("test", "test", 10m);
            _repository.GetAll().ReturnsForAnyArgs(Builder<Product>.CreateListOfSize(2).All().WithFactory(() => new Product("test", "test", 10m)).Build());

            //act
            var products = await _service.GetAll();

            //assert
            Assert.NotNull(products);
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public async Task ShouldNotifyWhenBasicExceptionOccurs()
        {
            //arrange
            _repository.When(p => p.GetAll()).Throw(new BasicTableException("test", Restaurante.Domain.Common.Enums.NotificationKeys.Error));

            //act
            var products = await _service.GetAll();

            //assert
            Assert.Null(products);
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenExceptionIsThrownGetAll()
        {
            //arrange
            _repository.When(p => p.GetAll()).Throw(new Exception("test"));

            //assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                //act
                _service.GetAll()
            );

            Assert.Equal("Houve um erro ao tentar recuperar os produtos!", ex.Message);
        }

        [Fact]
        public async Task ShouldReturnProduct()
        {
            //arrange
            _repository.Get(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(new Product("test", "test", 10m));

            //act
            var product = await _service.Get(1);

            //assert
            Assert.NotNull(product);
            Assert.Equal("test", product.Name);
            Assert.Equal("test", product.Description);
        }

        [Fact]
        public async Task ShouldNotifyWhenGetThrowsBasicException()
        {
            //arrange
            _repository.When(r => r.Get(Arg.Any<Expression<Func<Product, bool>>>())).Throw(new BasicTableException("test", Restaurante.Domain.Common.Enums.NotificationKeys.Error));

            //act
            var product = await _service.Get(1);

            //assert
            Assert.Null(product);
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
        }

        [Fact]
        public async Task ShouldThrowBasicExceptionWhenProductNotFound()
        {
            //arrange
            Product product = null;
            _repository.Get(Arg.Any<Expression<Func<Product, bool>>>()).ReturnsForAnyArgs(product);

            //act
            await _service.Get(1);

            //assert
            Assert.Null(product);
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
        }

        [Fact]
        public async Task ShouldThrowExceptionWhenExceptionIsThrownOnGet()
        {
            //arrange
            _repository.When(r => r.Get(Arg.Any<Expression<Func<Product, bool>>>())).Throw(new Exception());

            //assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                //act
                _service.Get(1)
            );

            //assert
            await _repository.ReceivedWithAnyArgs(1).Get(Arg.Any<Expression<Func<Product, bool>>>());
            Assert.Equal("Houve um erro ao tentar recuperar o produto!", ex.Message);
        }

        [Fact]
        public async Task ShouldUpdateProduct()
        {
            //arrange
            var productToUpdate = new Product("Test", "Test", 10m);
            _repository.Get(Arg.Any<Expression<Func<Product, bool>>>()).Returns(productToUpdate);
            _repository.Update(Arg.Any<Product>()).Returns(true);
            _basicEntitiesService.DeleteEntity(Arg.Any<Photo>()).Returns(true);
            _employeeDomainRepository.Get(Arg.Any<int>()).Returns(EmployeeMock.GetDefault());
            var product = new Product("Test update", "Test update descr", 10m, 10, new ProductCategory("Test"), "tes", "test", true);

            //act
            var success = await _service.UpdateProduct(1, product, 1);

            //assert
            Assert.True(success);
            await _repository.ReceivedWithAnyArgs(1).Update(Arg.Any<Product>());
            Assert.Equal(product.Name, productToUpdate.Name);
            Assert.Equal(product.Description, productToUpdate.Description);

        }

        [Fact]
        public async Task ShouldNotUpdateProductIfPhotoIsNotDeleted()
        {
            //arrange
            var productToUpdate = new Product("Test", "Test", 10m);
            _repository.Get(Arg.Any<Expression<Func<Product, bool>>>()).Returns(productToUpdate);
            _repository.Update(Arg.Any<Product>()).Returns(true);
            _basicEntitiesService.DeleteEntity(Arg.Any<Photo>()).Returns(false);
            _employeeDomainRepository.Get(Arg.Any<int>()).Returns(EmployeeMock.GetDefault());
            var product = new Product("Test update", "Test update descr", 10m, 10, new ProductCategory("Test"), "tes", "test", true);

            //act
            var success = await _service.UpdateProduct(1, product, 1);

            //assert
            Assert.False(success);
            await _repository.DidNotReceiveWithAnyArgs().Update(Arg.Any<Product>());
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());

        }

        [Fact]
        public async Task ShouldNotUpdateProductIfExceptionIsThrown()
        {
            //arrange
            var productToUpdate = new Product("Test", "Test", 10m);
            _repository.Get(Arg.Any<Expression<Func<Product, bool>>>()).Returns(productToUpdate);
            _repository.When(r => r.Update(Arg.Any<Product>())).Throw(new Exception());
            _basicEntitiesService.DeleteEntity(Arg.Any<Photo>()).Returns(true);
            _employeeDomainRepository.Get(Arg.Any<int>()).Returns(EmployeeMock.GetDefault());
            var product = new Product("Test update", "Test update descr", 10m, 10, new ProductCategory("Test"), "tes", "test", true);

            //assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                //act
                _service.UpdateProduct(1, product, 1)
            );

            //assert
            await _repository.ReceivedWithAnyArgs(1).Update(Arg.Any<Product>());
            Assert.Equal("Houve um erro ao tentar atualizar o produto!", ex.Message);
        }
    }
}
