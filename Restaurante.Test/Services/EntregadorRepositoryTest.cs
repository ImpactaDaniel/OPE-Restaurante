using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Entregadores.Repositories;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Users.Entregadores;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Restaurante.Test.Services
{
    public class EntregadorRepositoryTest
    {
        private readonly IEntregadorDomainRepository _repository;
        private readonly IRestauranteDbContext _context;

        public EntregadorRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new RestauranteDbContext(options);

            _repository = new EntregadoresRepository(_context);
        }

        [Fact]
        public async Task ShouldCreateNewEntregador()
        {
            //Arrange
            var entregador = EntregadorMock.GetDefaulEntregador();
            var usuario = FuncionarioMock.GetDefaultGerente();
            _context.Funcionarios.Add(usuario);
            await _context.SaveChangesAsync();

            //Act
            var ent = await _repository.CreateFuncionario(entregador, usuario);

            //Assert
            Assert.NotNull(ent);
        }
    }
}
