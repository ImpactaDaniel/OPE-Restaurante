using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Users.Entregadores.Services;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class EntregadorServiceTest
    {
        private readonly ILogger<EntregadorService> _logger;
        private readonly INotifier _notifier;
        private readonly IFuncionarioDomainRepository<Funcionario> _funcionarioDomainRepository;
        private readonly IMapper<Entregador, EntregadorIntegration> _mapper;
        private readonly IEntregadorIntegrationService _entregadorIntegrationService;
        private IEntregadoresService _entregadoresService;
        public EntregadorServiceTest()
        {
            _logger = Substitute.For<ILogger<EntregadorService>>();
            _notifier = Substitute.For<INotifier>();
            _funcionarioDomainRepository = Substitute.For<IFuncionarioDomainRepository<Funcionario>>();
            _mapper = Substitute.For<IMapper<Entregador, EntregadorIntegration>>();
            _entregadorIntegrationService = Substitute.For<IEntregadorIntegrationService>();
        }
        [Fact]
        public async Task ShouldCreateNewEntregador()
        {
            //arrange
            _funcionarioDomainRepository.Get(Arg.Any<int>()).ReturnsForAnyArgs(FuncionarioMock.GetDefaultGerente());
            _mapper.Map(Arg.Any<Entregador>()).ReturnsForAnyArgs(EntregadorIntegrationMock.GetDefault());            

            _entregadoresService = new EntregadorService(_notifier, _logger, _funcionarioDomainRepository, _entregadorIntegrationService, _mapper);

            //act
            var response = await _entregadoresService.CreateFuncionario(EntregadorMock.GetDefaulEntregador(), FuncionarioMock.GetDefaultGerente().Id);

            //assert
            Assert.True(response);
            await _entregadorIntegrationService.ReceivedWithAnyArgs().CreateNewEntregador(Arg.Any<EntregadorIntegration>());
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(Arg.Any<Notification>());
        }
    }
}
