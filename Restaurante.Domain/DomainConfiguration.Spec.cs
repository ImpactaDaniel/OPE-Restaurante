using Microsoft.Extensions.DependencyInjection;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using Xunit;

namespace Restaurante.Domain
{
    public class DomainConfigurationSpec
    {
        [Fact]
        public void AddDomainShouldRegisterFactories()
        {
            // Arrange
            var serviceCollection = new ServiceCollection();

            // Act
            var services = serviceCollection
                .AddDomain()
                .BuildServiceProvider();

            // Assert
            var funcionarioFactory = services.GetService<IFuncionarioFactory>();
            var entregadorFactory = services.GetService<IEntregadorFactory>();
            Assert.NotNull(entregadorFactory);
            Assert.NotNull(funcionarioFactory);
        }
    }
}
