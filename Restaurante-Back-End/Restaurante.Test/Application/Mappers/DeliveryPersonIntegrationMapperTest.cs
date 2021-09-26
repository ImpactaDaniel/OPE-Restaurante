using Restaurante.Application.Common.Data.Mappers;
using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Test.Usuarios.Mocks;
using System.Linq;
using Xunit;

namespace Restaurante.Test.Application.Mappers
{
    public class DeliveryPersonIntegrationMapperTest
    {
        private readonly IMapper<DeliveryPerson, DeliveryPersonIntegration> _mapper;
        public DeliveryPersonIntegrationMapperTest()
        {
            _mapper = new DeliveryPersonIntegrationMapper();
        }

        [Fact]
        public void ShouldMapCorrectly()
        {
            //arrange
            var deliveryPerson = EntregadorMock.GetDefaulEntregador();

            //act
            var deliveryIntegration = _mapper.Map(deliveryPerson);

            //assert
            Assert.Equal(deliveryPerson.Name, deliveryIntegration.Personal.Name);
            Assert.Equal(deliveryPerson.Email, deliveryIntegration.Personal.Email);
            Assert.Equal(deliveryPerson.Password, deliveryIntegration.User.Password);
            Assert.Equal(deliveryPerson.Account.AccountNumber, deliveryIntegration.Payment.Number);
            Assert.Equal(deliveryPerson.Account.Bank.Id.ToString(), deliveryIntegration.Payment.BankCode);
            Assert.Equal(deliveryPerson.Account.Branch, deliveryIntegration.Payment.AgencyCode);
            Assert.Contains(deliveryIntegration.Personal.Phone.Number, deliveryPerson.Phones.Select(p => p.PhoneNumber));
            Assert.Equal(deliveryPerson.MotoCycle.Brand, deliveryIntegration.Motorcycle.Brand);
            Assert.Equal(deliveryPerson.MotoCycle.Year, deliveryIntegration.Motorcycle.Year);
            Assert.Equal(deliveryPerson.MotoCycle.Model, deliveryIntegration.Motorcycle.Model);
            Assert.Equal(deliveryPerson.Address.City, deliveryIntegration.Personal.Address.City);
            Assert.Equal(deliveryPerson.Address.State, deliveryIntegration.Personal.Address.State);
            Assert.Equal(deliveryPerson.Address.Street, deliveryIntegration.Personal.Address.Street);
            Assert.Equal(deliveryPerson.Address.CEP, deliveryIntegration.Personal.Address.ZipCode);
            Assert.Equal(deliveryPerson.Address.Number, deliveryIntegration.Personal.Address.Number);
            Assert.Equal(deliveryPerson.BirthDate.ToShortDateString(), deliveryIntegration.Personal.BirthDate.ToShortDateString());
            Assert.Equal(deliveryPerson.Document, deliveryIntegration.Personal.CPF);
        }
    }
}
