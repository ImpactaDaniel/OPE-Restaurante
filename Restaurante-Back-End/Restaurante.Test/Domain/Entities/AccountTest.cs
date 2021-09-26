using Restaurante.Domain.BasicEntities.Exception;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Test.Usuarios.Mocks;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class AccountTest
    {
        [Fact]
        public void ShouldCreateNewAccountCorrectly()
        {
            //arrange
            var bank = BankMock.GetDefault();

            //act
            var account = new Account(bank, "branch", "number", 1);

            //assert
            Assert.NotNull(account);
        }

        [Fact]
        public void ShouldNotCreateAndThrowBasicTableExceptionWhenBankIsNull()
        {
            //assert
            Assert.Throws<BasicTableException>(() =>
                //act
                new Account(null, "branch", "number", 1));
        }

        [Theory]
        [InlineData("", "account", 1)]
        [InlineData("branch", "", 1)]
        public void ShouldThrowUserExceptionWhenParametersIsInvalid(string branch, string account, int digit)
        {
            //assert
            Assert.Throws<UserException>(() =>
                //act
                new Account(BankMock.GetDefault(), branch, account, digit));
        }

        [Fact]
        public void ShouldUpdateBankCorrectly()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);
            var expectedBank = new Bank("test", 10);

            //act
            account.UpdateBank(expectedBank);

            //assert
            Assert.Equal(expectedBank.Name, account.Bank.Name);
        }

        [Fact]
        public void ShouldThrowExceptionWhenBankIsNullOnUpdate()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);

            //assert
            var ex = Assert.Throws<BasicTableException>(() =>
                //act
                account.UpdateBank(null)
            );

            Assert.Equal("Banco não pode ser nulo!", ex.Message);
        }

        [Fact]
        public void ShouldNotUpdateWhenItsTheSameBankId()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);
            var expectedBank = new Bank("test");

            //act
            account.UpdateBank(expectedBank);

            //assert
            Assert.Equal(bank.Name, account.Bank.Name);
        }

        [Fact]
        public void ShouldUpdateBranchCorrectly()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);

            //act
            account.UpdateBranch("test update branch");

            //assert
            Assert.Equal("test update branch", account.Branch);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenTriedToUpdateEmptyBranch()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);

            //assert
            Assert.Throws<UserException>(() =>
                //act
                account.UpdateBranch(string.Empty)
            );
        }

        [Fact]
        public void ShouldUpdateAccountNumberCorrectly()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);

            //act
            account.UpdateAccountNumber("test update account number");

            //assert
            Assert.Equal("test update account number", account.AccountNumber);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenTriedToUpdateEmptyAccountNumber()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);

            //assert
            Assert.Throws<UserException>(() =>
                //act
                account.UpdateAccountNumber(string.Empty)
            );
        }

        [Fact]
        public void ShouldUpdateDigitCorrectly()
        {
            //arrange
            var bank = BankMock.GetDefault();
            var account = new Account(bank, "branch", "number", 1);

            //act
            account.UpdateDigit(3);

            //assert
            Assert.Equal(3, account.Digit);
        }
    }
}
