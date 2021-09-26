using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Restaurante.Domain.Encrypt.Intefaces;
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
        private readonly IPasswordEncrypt _passwordEncrypt;

        public FuncionarioRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<RestauranteDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _passwordEncrypt = Substitute.For<IPasswordEncrypt>();

            _passwordEncrypt.Encrypt(Arg.Any<string>())
                .Returns("teste password");

            _passwordEncrypt.Compare(Arg.Any<string>(), Arg.Any<string>())
                .Returns(true);

            _context = new RestauranteDbContext(options);

            _repository = new EmployeesRepository(_context, _passwordEncrypt);
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
            var ent = await _repository.CreateEmployee(funcionario);

            //Assert
            Assert.NotNull(ent);
        }
    }
}
