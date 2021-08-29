using NSubstitute;
using Restaurante.Application.Users.Create;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios;
using Restaurante.Domain.Users.Repositories.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Create.CreateFuncionarioRequest;

namespace Restaurante.Test.Application
{
    public class CreateFuncionarioRequestHandlerTest
    {
        private readonly IFuncionarioFactory<Funcionario> _factory;
        private readonly IFuncionarioDomainRepository<Funcionario> _repository;
        private readonly INotifier _notifier;

        public CreateFuncionarioRequestHandlerTest()
        {
            _factory = Substitute.For<IFuncionarioFactory<Funcionario>>();
            _factory.Build().ReturnsForAnyArgs(FuncionarioMock.GetDefault());

            _repository = Substitute.For<IFuncionarioDomainRepository<Funcionario>>();

            _notifier = Substitute.For<INotifier>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateFuncionarioRequestHandler(_factory, _repository, _notifier);
            var request = new CreateFuncionarioRequest(FuncionarioMock.GetDefault());
            _repository.CreateFuncionario(default, default).ReturnsForAnyArgs(FuncionarioMock.GetDefault());

            _notifier.HasNotifications().ReturnsForAnyArgs(false);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.NotNull(response.Result);
            Assert.True(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateFuncionarioRequestHandler(_factory, _repository, _notifier);
            var request = new CreateFuncionarioRequest(FuncionarioMock.GetDefault());

            _repository.CreateFuncionario(default, default).ReturnsForAnyArgs(default(Funcionario));

            _notifier.HasNotifications().ReturnsForAnyArgs(true);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.Null(response.Result);
            Assert.False(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
        }
    }
}
