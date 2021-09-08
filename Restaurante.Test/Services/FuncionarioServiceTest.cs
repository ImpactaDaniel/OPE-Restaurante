using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Users.Funcionarios.Services;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class FuncionarioServiceTest
    {
        private readonly INotifier _notifier;
        private IEmployeeDomainRepository<Employee> _repository;
        private readonly ILogger<EmployeeService<Employee>> _logger;

        public FuncionarioServiceTest()
        {
            _notifier = Substitute.For<INotifier>();
            _repository = Substitute.For<IEmployeeDomainRepository<Employee>>();
            _logger = Substitute.For<ILogger<EmployeeService<Employee>>>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var service = new EmployeeService<Employee>(_notifier, _repository, _logger);
            _repository.Get(default).ReturnsForAnyArgs(FuncionarioMock.GetDefaultGerente());

            //Act
            var response = await service.CreateEmployee(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente().Id);

            //Assert
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
            _logger.DidNotReceiveWithAnyArgs().LogError(default);
            Assert.True(response);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var service = new EmployeeService<Employee>(_notifier, _repository, _logger);

            _repository.When(repository =>
            {
                repository.Get(Arg.Any<int>());                
            }).Do((a) => {
                throw new Exception();
            });

            //Act
            var ex = await Assert.ThrowsAsync<Exception>(async () =>
                await service.CreateEmployee(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefault().Id)
            );

            //Assert
            _logger.ReceivedWithAnyArgs().Log(default, default, default, default, default, default);
        }

        [Fact]
        public async Task ShouldNotCreateWhenUserIsNull()
        {
            //Arrange
            var service = new EmployeeService<Employee>(_notifier, _repository, _logger);
            _repository.Get(default).ReturnsForAnyArgs(Task.FromResult<Employee>(null));

            //Act
            var response = await service.CreateEmployee(FuncionarioMock.GetDefault(), FuncionarioMock.GetDefaultGerente().Id);

            //Assert
            _notifier.ReceivedWithAnyArgs().AddNotification(default);
            Assert.False(response);
        }

    }
}
