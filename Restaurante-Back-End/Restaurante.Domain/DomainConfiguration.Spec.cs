using Microsoft.Extensions.DependencyInjection;
using Restaurante.Domain.Common.Factories.Interfaces;
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
            var funcionarioFactory = services.GetService<IEmployeeFactory>();
            var entregadorFactory = services.GetService<IDeliverFactory>();
            Assert.NotNull(entregadorFactory);
            Assert.NotNull(funcionarioFactory);
        }
    }
}
