using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using Restaurante.Infra.Common.Persistence;
using Restaurante.Infra.Common.Persistence.Interfaces;
using Restaurante.Infra.Users.Employees;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;
namespace Restaurante.Test.Services
{
    public class FuncionarioRepositoryTest
    {
        private readonly IEmployeeDomainRepository<Employee> _repository;
        private readonly IRestauranteDbContext _context;

        public FuncionarioRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new RestauranteDbContext(options);

            _repository = new EmployeesRepository(_context);
        }

        [Fact]
        public async Task ShouldCreateNewFuncionario()
        {
            //Arrange
            var funcionario = EmployeeMock.GetDefault();
            var usuario = EmployeeMock.GetDefaultManager();
            _context.Employees.Add(usuario);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            //Act
            var ent = await _repository.CreateEmployee(funcionario, usuario);

            //Assert
            Assert.NotNull(ent);
        }
    }
}
