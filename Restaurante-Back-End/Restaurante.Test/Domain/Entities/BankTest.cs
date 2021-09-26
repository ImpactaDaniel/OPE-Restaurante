using Restaurante.Domain.Users.Employees.Models;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class BankTest
    {
        [Fact]
        public void ShouldCreteNewBankCorrectly()
        {
            //arrange
            var name = "Test bank ctor";

            //act
            var bank = new Bank(name);

            //assert
            Assert.NotNull(bank);
            Assert.Equal(name, bank.Name);
        }

        [Fact]
        public void ShouldCreteNewBankWithIdCorrectly()
        {
            //arrange
            var name = "Test bank ctor";
            var id = 2;

            //act
            var bank = new Bank(name, id);

            //assert
            Assert.NotNull(bank);
            Assert.Equal(name, bank.Name);
            Assert.Equal(id, bank.Id);
        }

        [Fact]
        public void ShouldUpdateNameCorrectly()
        {
            //arrange
            var name = "Test bank ctor";
            var bank = new Bank(name);

            //act
            bank.UpdateName("teste bank update");

            //assert
            Assert.Equal("teste bank update", bank.Name);
        }
    }
}
