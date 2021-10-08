using NSubstitute;
using Restaurante.Application.Users.Common.Requests.Auth;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Common.Requests.Auth.ChangePasswordFirstAccessRequest;

namespace Restaurante.Test.Application.Requests
{
    public class ChangePasswordFirstAccessRequestHandlerTest
    {

        private readonly IEmployeesService<Employee> _service;
        public ChangePasswordFirstAccessRequestHandlerTest()
        {
            _service = Substitute.For<IEmployeesService<Employee>>();
        }

        [Fact]
        public async Task ShouldChangePassword()
        {
            //arrange
            _service.Get(Arg.Any<int>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service.Login(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service.Update(Arg.Any<Employee>()).ReturnsForAnyArgs(true);

            var handler = new ChangePasswordFirstAccessRequestHandler(_service);
            var request = new ChangePasswordFirstAccessRequest()
            {
                Id = 1,
                OldPassword = DataTest.PASSWORD,
                Password = DataTest.PASSWORD + "Teste"
            };

            //act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(response);
            Assert.NotNull(response.Result);
            Assert.True(response.Success);
        }

        [Fact]
        public async Task ShouldNotChangePassword()
        {
            //arrange
            Employee employe = null;
            _service.Get(Arg.Any<int>()).ReturnsForAnyArgs(employe);

            _service.Login(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());

            var handler = new ChangePasswordFirstAccessRequestHandler(_service);
            var request = new ChangePasswordFirstAccessRequest()
            {
                Id = 1,
                OldPassword = DataTest.PASSWORD,
                Password = DataTest.PASSWORD + "teste"
            };

            //act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task ShouldNotChangePasswordWhenLoginFails()
        {
            //arrange
            Employee employe = null;
            _service.Get(Arg.Any<int>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service.Login(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(employe);

            var handler = new ChangePasswordFirstAccessRequestHandler(_service);
            var request = new ChangePasswordFirstAccessRequest()
            {
                Id = 1,
                OldPassword = DataTest.PASSWORD,
                Password = DataTest.PASSWORD + "teste"
            };

            //act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Null(response.Result);
        }

        [Fact]
        public async Task ShouldNotChangePasswordWhenUpdateFails()
        {
            //arrange
            _service.Get(Arg.Any<int>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service.Login(Arg.Any<string>(), Arg.Any<string>()).ReturnsForAnyArgs(EmployeeMock.GetDefault());

            _service.Update(Arg.Any<Employee>()).ReturnsForAnyArgs(false);

            var handler = new ChangePasswordFirstAccessRequestHandler(_service);
            var request = new ChangePasswordFirstAccessRequest()
            {
                Id = 1,
                OldPassword = DataTest.PASSWORD,
                Password = DataTest.PASSWORD + "teste"
            };

            //act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Null(response.Result);
        }
    }
}
