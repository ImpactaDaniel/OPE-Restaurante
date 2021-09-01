using Restaurante.Domain.Common.Factories;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Test.Usuarios.Mocks;
using Xunit;
namespace Restaurante.Test.Usuarios
{
    public class EntregadorTest
    {
        IEntregadorFactory _factory;
        public EntregadorTest()
        {
            _factory = new EntregadorFactory();
        }

        [Fact]
        public void DeveCriarUmNovoEntregadorQuandoInformacoesForemValidas()
        {
            //Arrange
            _factory
                .WithVehicle(EntregadorMock.GetDefaultVehicle())
                .WithName("Daniel")
                .WithEmail("daniel@gmail.com")
                .WithPassword("123456");

            //Act
            var entregador = _factory.Build();

            //Assert
            Assert.NotNull(entregador);
            Assert.Equal("Daniel", entregador.Name);
        }

        //Arrange
        [Theory]
        [ClassData(typeof(EntregadoresInvalidos))]
        public void DeveRetornarUserExceptionQuandoInformacoesInvalidas(string nome,
                                                                            string email,
                                                                            string password,
                                                                            Veiculo veiculo,
                                                                            string mensagemEsperada)
        {

            //Assert
            var ex = Assert.Throws<UserException>(() =>
                _factory
                .WithVehicle(veiculo)
                .WithName(nome)
                .WithEmail(email)
                .WithPassword(password)
            );

            Assert.Equal(mensagemEsperada, ex.Message);
        }
    }
}
