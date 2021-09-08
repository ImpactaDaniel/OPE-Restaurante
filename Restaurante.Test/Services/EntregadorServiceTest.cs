using Microsoft.Extensions.Logging;
using NSubstitute;
using Restaurante.Application.Users.Delivers.Services;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Entregadores.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class EntregadorServiceTest
    {
        private readonly ILogger<DeliverService> _logger;
        private readonly INotifier _notifier;
        private readonly IEmployeeDomainRepository<Employee> _funcionarioDomainRepository;
        private readonly IMapper<Deliver, EntregadorIntegration> _mapper;
        private readonly IEntregadorIntegrationService _entregadorIntegrationService;
        private IDeliversService _entregadoresService;
        public EntregadorServiceTest()
        {
            _logger = Substitute.For<ILogger<DeliverService>>();
            _notifier = Substitute.For<INotifier>();
            _funcionarioDomainRepository = Substitute.For<IEmployeeDomainRepository<Employee>>();
            _mapper = Substitute.For<IMapper<Deliver, EntregadorIntegration>>();
            _entregadorIntegrationService = Substitute.For<IEntregadorIntegrationService>();
        }
        [Fact]
        public async Task ShouldCreateNewEntregador()
        {
            //arrange
            _funcionarioDomainRepository.Get(Arg.Any<int>()).ReturnsForAnyArgs(FuncionarioMock.GetDefaultGerente());
            _mapper.Map(Arg.Any<Deliver>()).ReturnsForAnyArgs(EntregadorIntegrationMock.GetDefault());            

            _entregadoresService = new DeliverService(_notifier, _logger, _funcionarioDomainRepository, _entregadorIntegrationService, _mapper);

            //act
            var response = await _entregadoresService.CreateEmployee(EntregadorMock.GetDefaulEntregador(), FuncionarioMock.GetDefaultGerente().Id);

            //assert
            Assert.True(response);
            await _entregadorIntegrationService.ReceivedWithAnyArgs().CreateNewEntregador(Arg.Any<EntregadorIntegration>());
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(Arg.Any<Notification>());
        }
    }
}
