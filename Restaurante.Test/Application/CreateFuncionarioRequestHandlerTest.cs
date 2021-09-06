using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Common.Models;
using Restaurante.Application.Users.Funcionarios.Requests.Create;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;
using static Restaurante.Application.Users.Funcionarios.Requests.Create.CreateFuncionarioRequest;

namespace Restaurante.Test.Application
{
    public class CreateFuncionarioRequestHandlerTest
    {
        private readonly IFuncionarioFactory _factory;
        private readonly IFuncionarioService<Funcionario> _service;
        private readonly IMessageSenderService<EmailMessage> _emailService;
        private readonly INotifier _notifier;
        private readonly ILogger<CreateFuncionarioRequestHandler> _logger;

        public CreateFuncionarioRequestHandlerTest()
        {
            _factory = Substitute.For<IFuncionarioFactory>();
            _factory.Build().ReturnsForAnyArgs(FuncionarioMock.GetDefault());

            _service = Substitute.For<IFuncionarioService<Funcionario>>();

            _emailService = Substitute.For<IMessageSenderService<EmailMessage>>();

            _logger = Substitute.For<ILogger<CreateFuncionarioRequestHandler>>();

            _notifier = Substitute.For<INotifier>();
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateFuncionarioRequestHandler(_factory, _service, _notifier, _logger, _emailService);
            var funcionarioDefault = FuncionarioMock.GetDefault();
            var request = new CreateFuncionarioRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type
            };
            _service.CreateFuncionario(default, default).ReturnsForAnyArgs(true);

            _notifier.HasNotifications().ReturnsForAnyArgs(false);

            //Act
            var response = await handler.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(response.Result);
            Assert.True(response.Success);
            _notifier.ReceivedWithAnyArgs().HasNotifications();
            await _emailService.ReceivedWithAnyArgs().SendAsync(Arg.Any<EmailMessage>());
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
        }

        [Fact]
        public async Task ShouldNotCreateNewFuncionario()
        {
            //Arrange
            var handler = new CreateFuncionarioRequestHandler(_factory, _service, _notifier, _logger, _emailService);
            var funcionarioDefault = FuncionarioMock.GetDefault();

            var request = new CreateFuncionarioRequest
            {
                Name = funcionarioDefault.Name,
                Email = funcionarioDefault.Email,
                Password = funcionarioDefault.Password,
                Type = funcionarioDefault.Type
            };

            _service.CreateFuncionario(Arg.Any<Funcionario>(), default).ReturnsForAnyArgs(false);

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
