using NSubstitute;
using Restaurante.Application.Users.Create;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Create.CreateFuncionarioRequest;

namespace Restaurante.Test.Application
{
    public class CreateFuncionarioRequestHandlerTest
    {
        private readonly IFuncionarioFactory<Funcionario> _factory;
        private readonly IFuncionarioService<Funcionario> _service;
        private readonly INotifier _notifier;

        public CreateFuncionarioRequestHandlerTest()
        {
            _factory = Substitute.For<IFuncionarioFactory<Funcionario>>();
            _factory.Build().ReturnsForAnyArgs(FuncionarioMock.GetDefault());

            _service = Substitute.For<IFuncionarioService<Funcionario>>();

            _notifier = Substitute.For<INotifier>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateFuncionarioRequestHandler(_factory, _service, _notifier);
            var request = new CreateFuncionarioRequest(FuncionarioMock.GetDefault());
            _service.CreateFuncionario(default, default).ReturnsForAnyArgs(true);

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
            var handler = new CreateFuncionarioRequestHandler(_factory, _service, _notifier);
            var request = new CreateFuncionarioRequest(FuncionarioMock.GetDefault());

            _service.CreateFuncionario(default, default).ReturnsForAnyArgs(false);

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
