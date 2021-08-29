using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Funcionarios.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Users.Funcionarios;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Restaurante.Test.Services
{
    public class FuncionarioRepositoryTest
    {
        private readonly IFuncionarioDomainRepository<Funcionario> _repository;
        private readonly IRestauranteDbContext _context;

        public FuncionarioRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new RestauranteDbContext(options);

            _repository = new FuncionariosRepository(_context);
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
            Assert.NotNull(ent);
        }
    }
}
