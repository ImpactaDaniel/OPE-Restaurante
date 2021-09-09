using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Users.Employees.Requests.Create;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Employees.Requests.Create.CreateEmployeeRequest;

namespace Restaurante.Test.Application
{
    public class CreateFuncionarioRequestHandlerTest
    {
        private readonly IEmployeeFactory _factory;
        private readonly IEmployeesService<Employee> _service;
        private readonly INotifier _notifier;
        private readonly ILogger<CreateEmployeeRequestHandler> _logger;

        public CreateFuncionarioRequestHandlerTest()
        {
            _factory = Substitute.For<IEmployeeFactory>();
            _factory.Build().ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service = Substitute.For<IEmployeesService<Employee>>();

            _logger = Substitute.For<ILogger<CreateEmployeeRequestHandler>>();

            _notifier = Substitute.For<INotifier>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateEmployeeRequestHandler(_factory, _service, _notifier, _logger);
            var funcionarioDefault = EmployeeMock.GetDefault();
            var request = new CreateEmployeeRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type
            };
            _service.CreateEmployee(default, default).ReturnsForAnyArgs(true);

            _notifier.HasNotifications().ReturnsForAnyArgs(false);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(response.Result);
            Assert.True(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateEmployeeRequestHandler(_factory, _service, _notifier, _logger);
            var funcionarioDefault = EmployeeMock.GetDefault();

            var request = new CreateEmployeeRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type
            };

            _service.CreateEmployee(Arg.Any<Employee>(), default).ReturnsForAnyArgs(false);

            _notifier.HasNotifications().ReturnsForAnyArgs(true);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.False(response.Result);
            Assert.False(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
        }
    }
}
