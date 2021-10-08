using Restaurante.Domain.Common.Factories;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Collections.Generic;
using Xunit;
namespace Restaurante.Test.Usuarios
{
    public class EntregadorTest
    {
        private readonly IDeliverFactory _factory;
        public EntregadorTest()
        {
            _factory = new DeliverFactory();
        }

        [Fact]
        public void ShouldCreateNewDeliveryManWhenValidInfos()
        {
            //Arrange
            _factory
                .WithVehicle(EntregadorMock.GetDefaultVehicle())
                .WithAccount(AccountMock.GetDefault())
                .WithAddress(AddressMock.GetDefault())
                .WithDocument(DataTest.CPF)
                .WithBirthDate(DateTime.Now)
                .WithPhone(PhoneMock.GetDefault())
                .WithName("Daniel")
                .WithEmail(DataTest.EMAIL)
                .WithPassword(DataTest.PASSWORD);

            //Act
            var entregador = _factory.Build();

            //Assert
            Assert.NotNull(entregador);
            Assert.Equal("Daniel", entregador.Name);
        }


        [Fact]
        public void ShouldCreteNewDeliverWhenItsValid()
        {
            //Arrange
            _factory
                .WithVehicle(EntregadorMock.GetDefaultVehicle())
                .WithAccount(AccountMock.GetDefault())
                .WithDocument(DataTest.CPF)
                .WithBirthDate(DateTime.Now)
                .WithAddress(AddressMock.GetDefault())
                .WithPhones(new List<Phone> { PhoneMock.GetDefault() })
                .WithName("Daniel")
                .WithEmail(DataTest.EMAIL)
                .WithPassword(DataTest.PASSWORD);

            //Act
            var entregador = _factory.Build();

            //Assert
            Assert.NotNull(entregador);
            Assert.Equal("Daniel", entregador.Name);
        }

        //Arrange
        [Theory]
        [ClassData(typeof(EntregadoresInvalidos))]
        public void ShouldThrowUserExceptionWhenInvalidInfos(string nome,
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
