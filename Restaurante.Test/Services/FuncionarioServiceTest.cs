using NSubstitute;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Domain.Users.Funcionarios.Services;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class FuncionarioServiceTest
    {
        private readonly INotifier _notifier;
        private readonly IFuncionarioDomainRepository<Funcionario> _repository;

        public FuncionarioServiceTest()
        {
            _notifier = Substitute.For<INotifier>();
            _repository = Substitute.For<IFuncionarioDomainRepository<Funcionario>>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var service = new FuncionarioService<Funcionario>(_notifier, _repository);
            _repository.Get(default).ReturnsForAnyArgs(FuncionarioMock.GetDefaultGerente());

            //Act
            var response = await service.CreateFuncionario(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente());

            //Assert
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
            Assert.True(response);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var service = new FuncionarioService<Funcionario>(_notifier, _repository);
            _repository.Get(default).ReturnsForAnyArgs(FuncionarioMock.GetDefault());

            //Act
            var response = await service.CreateFuncionario(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente());

            //Assert
            _notifier.ReceivedWithAnyArgs().AddNotification(default);
            Assert.False(response);
        }

        [Fact]
        public async Task ShouldNotCreateWhenUserIsNull()
        {
            //Arrange
            var service = new FuncionarioService<Funcionario>(_notifier, _repository);
            _repository.Get(default).ReturnsForAnyArgs(Task.FromResult<Funcionario>(null));

            //Act
            var response = await service.CreateFuncionario(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente());

            //Assert
            _notifier.ReceivedWithAnyArgs().AddNotification(default);
            Assert.False(response);
        }
    }
}
