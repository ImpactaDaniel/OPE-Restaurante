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
        IDeliverFactory _factory;
        public EntregadorTest()
        {
            _factory = new DeliverFactory();
        }

        [Fact]
        public void DeveCriarUmNovoEntregadorQuandoInformacoesForemValidas()
        {
            //Arrange
            _factory                
                .WithVehicle(EntregadorMock.GetDefaultVehicle())                
                .WithAccount(AccountMock.GetDefault())
                .WithAddress(AddressMock.GetDefault())
                .WithPhone(PhoneMock.GetDefault())                
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
                                                                            Vehicle veiculo,
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
