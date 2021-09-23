using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.BasicEntities.Services;
using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class BasicEntitiesServiceTest
    {
        private readonly IDefaultDomainRepository _defaultRepository;
        private readonly IBasicEntitiesService _basicEntitiesService;
        private readonly INotifier _notifier;
        private readonly ILogger<BasicEntitiesService> _logger;

        public BasicEntitiesServiceTest()
        {
            _defaultRepository = Substitute.For<IDefaultDomainRepository>();

            _defaultRepository.Get(Arg.Any<Expression<Func<Bank, bool>>>())
                .Returns(BankMock.GetDefault());

            _defaultRepository.GetAll<Bank>()
                .Returns(new List<Bank> { BankMock.GetDefault() });

            _defaultRepository.Create(Arg.Any<Bank>())
                .Returns(BankMock.GetDefault());

            _notifier = Substitute.For<INotifier>();

            _logger = Substitute.For<ILogger<BasicEntitiesService>>();

            _basicEntitiesService = new BasicEntitiesService(_defaultRepository, _notifier, _logger);
        }

        [Fact]
        public async Task ShouldReturnBasicEntity()
        {
            //Arrange
            Expression<Func<Bank, bool>> expression = b => b.Id == 1;

            //Assert
            var bank = await _basicEntitiesService.GetEntity(expression);

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).Get(Arg.Any<Expression<Func<Bank, bool>>>());
            Assert.Equal(BankMock.GetDefault(), bank);
        }

        [Fact]
        public async Task ShouldAddNewNotificationWhenGetThrowsBasicEntityException()
        {
            //Arrange
            Expression<Func<Bank, bool>> expression = b => b.Id == 1;
            _defaultRepository.Get(Arg.Any<Expression<Func<Bank, bool>>>())
                .Returns<Bank>(x => { throw new BasicTableException("", NotificationKeys.InvalidEntity); });

            //Assert
            var bank = await _basicEntitiesService.GetEntity(expression);

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).Get(Arg.Any<Expression<Func<Bank, bool>>>());
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
            Assert.Null(bank);
        }

        [Fact]
        public async Task ShouldLogAndReturnNullWhenGetThrowsException()
        {
            //Arrange
            Expression<Func<Bank, bool>> expression = b => b.Id == 1;
            _defaultRepository.Get(Arg.Any<Expression<Func<Bank, bool>>>())
                .Returns<Bank>(x => { throw new Exception(); });

            //Assert
            var bank = await _basicEntitiesService.GetEntity(expression);

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).Get(Arg.Any<Expression<Func<Bank, bool>>>());
            _notifier.DidNotReceive().AddNotification(Arg.Any<Notification>());
            Assert.Null(bank);
        }

        [Fact]
        public async Task ShouldReturnBasicEntities()
        {
            //Arrange
            var expectedBank = BankMock.GetDefault();

            //Assert
            var banks = await _basicEntitiesService.GetEntities<Bank>();

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).GetAll<Bank>();
            Assert.Contains(expectedBank, banks);
        }

        [Fact]
        public async Task ShouldAddNewNotificationWhenGetAllThrowsBasicEntityException()
        {
            //Arrange
            _defaultRepository.GetAll<Bank>()
                .Returns<IEnumerable<Bank>>(x => { throw new BasicTableException("", NotificationKeys.InvalidEntity); });

            //Assert
            var bank = await _basicEntitiesService.GetEntities<Bank>();

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).GetAll<Bank>();
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
            Assert.Null(bank);
        }

        [Fact]
        public async Task ShouldReturnNullWhenGetAllThrowsException()
        {
            //Arrange
            _defaultRepository.GetAll<Bank>()
                .Returns<IEnumerable<Bank>>(x => { throw new Exception(); });

            //Assert
            var bank = await _basicEntitiesService.GetEntities<Bank>();

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).GetAll<Bank>();
            _notifier.DidNotReceive().AddNotification(Arg.Any<Notification>());
            Assert.Null(bank);
        }

        [Fact]
        public async Task ShouldCreateBasicEntity()
        {
            //Arrange
            var bank = BankMock.GetDefault();

            //Assert
            var bankReceived = await _basicEntitiesService.CreateEntity(bank);

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).Create(Arg.Any<Bank>());
            Assert.Equal(bank, bankReceived);
        }

        [Fact]
        public async Task ShouldAddNewNotificationWhenCreateThrowsBasicEntityException()
        {
            //Arrange
            _defaultRepository.Create(Arg.Any<Bank>())
                .Returns<Bank>(x => { throw new BasicTableException("", NotificationKeys.InvalidEntity); });

            //Assert
            var bank = await _basicEntitiesService.CreateEntity(BankMock.GetDefault());

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).Create(Arg.Any<Bank>());
            _notifier.ReceivedWithAnyArgs(1).AddNotification(Arg.Any<Notification>());
            Assert.Null(bank);
        }

        [Fact]
        public async Task ShouldReturnNullWhenCreateThrowsException()
        {
            //Arrange
            _defaultRepository.Create(Arg.Any<Bank>())
                .Returns<Bank>(x => { throw new Exception(); });

            //Assert
            var bank = await _basicEntitiesService.CreateEntity(BankMock.GetDefault());

            //Act
            await _defaultRepository.ReceivedWithAnyArgs(1).Create(Arg.Any<Bank>());
            _notifier.DidNotReceive().AddNotification(Arg.Any<Notification>());
            Assert.Null(bank);
        }
    }
}
