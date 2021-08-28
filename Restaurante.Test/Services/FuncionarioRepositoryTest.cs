using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios;
using Restaurante.Domain.Users.Repositories.Interfaces;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Users.Entregadores;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Restaurante.Test.Services
{
    public class FuncionarioRepositoryTest
    {
        private readonly IFuncionarioDomainRepository<Funcionario> _repository;
        private readonly INotifier _notifier;
        private readonly IRestauranteDbContext _context;

        public FuncionarioRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new RestauranteDbContext(options);

            _notifier = Substitute.For<INotifier>();

            _repository = new FuncionariosRepository(_context, _notifier);
        }

        [Fact]
        public async Task ShouldCreateNewEntregador()
        {
            //Arrange
            var funcionario = FuncionarioMock.GetDefault();
            var usuario = FuncionarioMock.GetDefaultGerente();
            _context.Funcionarios.Add(usuario);
            await _context.SaveChangesAsync();

            //Act
            var ent = await _repository.CreateFuncionario(funcionario, usuario);

            //Assert
            _notifier.DidNotReceiveWithAnyArgs().AddNotification(default);
            Assert.NotNull(ent);
        }

        [Fact]
        public async Task ShouldNotCreateNewEntregador()
        {
            //Arrange
            var funcionario = EntregadorMock.GetDefaulEntregador();
            var usuario = FuncionarioMock.GetDefault();
            _context.Funcionarios.Add(usuario);
            await _context.SaveChangesAsync();

            //Act
            var ent = await _repository.CreateFuncionario(funcionario, usuario);

            //Assert
            _notifier.ReceivedWithAnyArgs().AddNotification(default);
            Assert.Null(ent);
        }
    }
}
