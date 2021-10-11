using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Products.Services;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Products.Models;
using Restaurante.Domain.Products.Repositories.Interfaces;
using Restaurante.Domain.Products.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class ProductServiceTests
    {
        private readonly IProductService _service;
        private readonly IProductDomainRepository _repository;
        private readonly IEmployeeDomainRepository<Employee> _employeeDomainRepository;
        private readonly INotifier _notifier;

        public ProductServiceTests()
        {
            _repository = Substitute.For<IProductDomainRepository>();
            _employeeDomainRepository = Substitute.For<IEmployeeDomainRepository<Employee>>();
            _notifier = Substitute.For<INotifier>();
            var logger = Substitute.For<ILogger<ProductService>>();

            _service = new ProductService(_repository, _notifier, logger, _employeeDomainRepository);
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
    }
}
