using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Users.Funcionarios.Services;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class FuncionarioServiceTest
    {
        private readonly INotifier _notifier;
        private IFuncionarioDomainRepository<Funcionario> _repository;
        private readonly ILogger<FuncionarioService<Funcionario>> _logger;

        public FuncionarioServiceTest()
        {
            _notifier = Substitute.For<INotifier>();
            _repository = Substitute.For<IFuncionarioDomainRepository<Funcionario>>();
            _logger = Substitute.For<ILogger<FuncionarioService<Funcionario>>>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var service = new FuncionarioService<Funcionario>(_notifier, _repository, _logger);
            _repository.Get(default).ReturnsForAnyArgs(FuncionarioMock.GetDefaultGerente());

            //Act
            var response = await service.CreateFuncionario(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente().Id);

            //Assert
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
            _logger.DidNotReceiveWithAnyArgs().LogError(default);
            Assert.True(response);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var service = new FuncionarioService<Funcionario>(_notifier, _repository, _logger);

            _repository.When(repository =>
            {
                repository.Get(Arg.Any<int>());                
            }).Do((a) => {
                throw new Exception();
            });

            //Act
            var ex = await Assert.ThrowsAsync<Exception>(async () =>
                await service.CreateFuncionario(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefault().Id)
            );

            //Assert
            _logger.ReceivedWithAnyArgs().Log(default, default, default, default, default, default);
        }

        [Fact]
        public async Task ShouldNotCreateWhenUserIsNull()
        {
            //Arrange
            var service = new FuncionarioService<Funcionario>(_notifier, _repository, _logger);
            _repository.Get(default).ReturnsForAnyArgs(Task.FromResult<Funcionario>(null));

            //Act
            var response = await service.CreateFuncionario(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente().Id);

            //Assert
            _notifier.ReceivedWithAnyArgs().AddNotification(default);
            Assert.False(response);
        }

    }
}
