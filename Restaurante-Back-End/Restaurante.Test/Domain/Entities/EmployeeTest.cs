using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Collections.Generic;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class EmployeeTest
    {
        [Fact]
        public void ShouldThrowUserExceptionOnAccountNull()
        {
            //assert
            var ex = Assert.Throws<UserException>(() =>
            //act
                new Employee("teste", "teste", "teste", Restaurante.Domain.Users.Enums.EmployeesType.Deliver,
                                null, new List<Phone>() { PhoneMock.GetDefault() }, AddressMock.GetDefault(), "4256456", DateTime.Now)
            );

            Assert.Equal("Conta não pode ser nula!", ex.Message);
            Assert.Equal(NotificationKeys.InvalidEntity, ex.Code);
        }

        [Fact]
        public void ShouldThrowUserExceptionOnPhonesNull()
        {
            //assert
            var ex = Assert.Throws<UserException>(() =>
            //act
                new Employee("teste", "teste", "teste", Restaurante.Domain.Users.Enums.EmployeesType.Deliver,
                                AccountMock.GetDefault(), null, AddressMock.GetDefault(), "25337808", DateTime.Now)
            );

            Assert.Equal("Telefones não podem ser nulos!", ex.Message);
            Assert.Equal(NotificationKeys.InvalidEntity, ex.Code);
        }

        [Fact]
        public void ShouldThrowUserExceptionOnPhonesEmpty()
        {
            //assert
            var ex = Assert.Throws<UserException>(() =>
            //act
                new Employee("teste", "teste", "teste", Restaurante.Domain.Users.Enums.EmployeesType.Deliver,
                                AccountMock.GetDefault(), new List<Phone>(), AddressMock.GetDefault(), "25337808", DateTime.Now)
            );

            Assert.Equal("Telefones não podem ser nulos!", ex.Message);
            Assert.Equal(NotificationKeys.InvalidEntity, ex.Code);
        }

        [Fact]
        public void ShouldThrowUserExceptionOnAddressNull()
        {
            //assert
            var ex = Assert.Throws<UserException>(() =>
            //act
                new Employee("teste", "teste", "teste", Restaurante.Domain.Users.Enums.EmployeesType.Deliver,
                                AccountMock.GetDefault(), new List<Phone> { PhoneMock.GetDefault() }, null, "2564", DateTime.Now)
            );

            Assert.Equal("Endereço não pode ser nulo!", ex.Message);
            Assert.Equal(NotificationKeys.InvalidEntity, ex.Code);
        }

        [Fact]
        public void ShouldUpdateTypeCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedType = EmployeesType.Manager;

            //act
            employee.UpdateType(expectedType);

            //assert
            Assert.Equal(expectedType, employee.Type);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenTryToMakeAnEmployeeAnDeliveryman()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();

            //assert

            var ex = Assert.Throws<UserException>(() =>
                //act
                employee.UpdateType(EmployeesType.Deliver)
            );

            Assert.Equal("Esse funcionário não pode ser entregador!", ex.Message);
        }

        [Fact]
        public void ShouldUpdateNameCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedName = "Test update name employee";

            //act
            employee.UpdateName(expectedName);

            //assert
            Assert.Equal(expectedName, employee.Name);
        }

        [Fact]
        public void ShouldUpdateEmailCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedEmail = "teste@update-employee.com";

            //act
            employee.UpdateEmail(expectedEmail);

            //assert
            Assert.Equal(expectedEmail, employee.Email);
        }

        [Fact]
        public void ShouldUpdatePasswordCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedPassword = "test update password";

            //act
            employee.UpdatePassword(expectedPassword);

            //assert
            Assert.Equal(expectedPassword, employee.Password);
        }

        [Fact]
        public void ShouldUpdateAccountCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedAccount = new Account(BankMock.GetDefault(), "Teste branch update", "account test update", 1, 10);

            //act
            employee.UpdateAccount(expectedAccount);

            //assert
            Assert.Equal(expectedAccount, employee.Account);
        }

        [Fact]
        public void ShouldThrowUserExceptionWhenAccountIsNullOnUpdate()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();

            //assert
            var ex = Assert.Throws<UserException>(() => 

                //act
                employee.UpdateAccount(null)
            );

            Assert.Equal("Conta não pode ser nula!", ex.Message);
        }

        [Fact]
        public void ShouldUpdateDocumentCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expDocument = "25337808";

            //act
            employee.UpdateDocument(expDocument);

            //assert
            Assert.Equal(expDocument, employee.Document);
        }

        [Fact]
        public void ShouldUpdateBirthDateCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedDate = new DateTime(2000, 08, 23);

            //act
            employee.UpdateBirthDate(expectedDate);

            //assert
            Assert.Equal(expectedDate.ToShortDateString(), employee.BirthDate.ToShortDateString());
        }
    }
}
