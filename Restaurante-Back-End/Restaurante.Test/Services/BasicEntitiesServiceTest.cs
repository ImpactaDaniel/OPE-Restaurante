using NSubstitute;
using Restaurante.Application.BasicEntities.Services;
using Restaurante.Domain.BasicEntities.Services.Interfaces;
using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class BasicEntitiesServiceTest
    {
        private readonly IDefaultDomainRepository _defaultRepository;
        private readonly IBasicEntitiesService _basicEntitiesService;

        public BasicEntitiesServiceTest()
        {
            _defaultRepository = Substitute.For<IDefaultDomainRepository>();

            _defaultRepository.Get(Arg.Any<Expression<Func<Bank, bool>>>())
                .Returns(BankMock.GetDefault());

            _defaultRepository.Create(Arg.Any<Bank>())
                .Returns(BankMock.GetDefault());

            var notifier = Substitute.For<INotifier>();

            _basicEntitiesService = new BasicEntitiesService(_defaultRepository, notifier);
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
    }
}
