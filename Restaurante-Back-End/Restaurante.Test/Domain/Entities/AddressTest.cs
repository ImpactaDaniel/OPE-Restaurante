using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Test.Usuarios.Mocks;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class AddressTest
    {
        [Theory]
        [InlineData("", "", "", "", "", "")]
        [InlineData("Angelo", "102", "Jaragua", "02998190", "SP", "")]
        [InlineData("Angelo", "102", "Jaragua", "02998190", "", "São Paulo")]
        [InlineData("Angelo", "102", "Jaragua", "", "SP", "São Paulo")]
        [InlineData("Angelo", "102", "", "02998190", "SP", "São Paulo")]
        [InlineData("Angelo", "", "Jaragua", "02998190", "SP", "São Paulo")]
        [InlineData("", "102", "Jaragua", "02998190", "SP", "São Paulo")]
        public void ShouldThrowUserExceptionWhenTriedToCreateInvalidAddress(string street, string number, string district, string cep, string state, string city)
        {
            //assert
            Assert.Throws<UserException>(() =>
                //act
                new Address(street, number, district, cep, state, city
                )
            );
        }

        [Fact]
        public void ShouldUpdateStreetCorrectly()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //act
            address.UpdateStreet("test update street");

            //assert
            Assert.Equal("test update street", address.Street);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenStreetIsNull()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //assert
            Assert.Throws<UserException>(() =>
                //act
                address.UpdateStreet(string.Empty)
            );
        }

        [Fact]
        public void ShouldUpdateNumberCorrectly()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //act
            address.UpdateNumber("test update number");

            //assert
            Assert.Equal("test update number", address.Number);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenNumberIsNull()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //assert
            Assert.Throws<UserException>(() =>
                //act
                address.UpdateNumber(string.Empty)
            );
        }

        [Fact]
        public void ShouldUpdateDistrictCorrectly()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //act
            address.UpdateDistrict("test update district");

            //assert
            Assert.Equal("test update district", address.District);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenDistrictIsNull()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //assert
            Assert.Throws<UserException>(() =>
                //act
                address.UpdateDistrict(string.Empty)
            );
        }

        [Fact]
        public void ShouldUpdateCEPCorrectly()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //act
            address.UpdateCEP("00000190");

            //assert
            Assert.Equal("00000190", address.CEP);
        }

        [Theory]
        [InlineData("")]
        [InlineData("029")]
        [InlineData("ddddddddd")]
        [InlineData("0299812")]
        public void ShouldThrowUserExceptionWhenCEPIsInvalid(string cep)
        {
            //arrange
            var address = AddressMock.GetDefault();

            //assert
            Assert.Throws<UserException>(() =>
                //act
                address.UpdateCEP(cep)
            );
        }


        [Fact]
        public void ShouldUpdateCityCorrectly()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //act
            address.UpdateCity("test update city");

            //assert
            Assert.Equal("test update city", address.City);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenCityIsNull()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //assert
            Assert.Throws<UserException>(() =>
                //act
                address.UpdateCity(string.Empty)
            );
        }

        [Fact]
        public void ShouldUpdateStateCorrectly()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //act
            address.UpdateState("test update state");

            //assert
            Assert.Equal("test update state", address.State);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenStateIsNull()
        {
            //arrange
            var address = AddressMock.GetDefault();

            //assert
            Assert.Throws<UserException>(() =>
                //act
                address.UpdateState(string.Empty)
            );
        }
    }
}
