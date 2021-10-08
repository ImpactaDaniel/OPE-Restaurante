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
                new Employee("teste", DataTest.EMAIL, DataTest.PASSWORD, EmployeesType.Deliver,
                                null, new List<Phone>() { PhoneMock.GetDefault() }, AddressMock.GetDefault(), DataTest.CPF, DateTime.Now)
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
                new Employee("teste", DataTest.EMAIL, DataTest.PASSWORD, EmployeesType.Deliver,
                                AccountMock.GetDefault(), null, AddressMock.GetDefault(), DataTest.CPF, DateTime.Now)
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
                new Employee("teste", DataTest.EMAIL, DataTest.PASSWORD, EmployeesType.Deliver,
                                AccountMock.GetDefault(), new List<Phone>(), AddressMock.GetDefault(), DataTest.CPF, DateTime.Now)
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
                new Employee("teste", DataTest.EMAIL, DataTest.PASSWORD, EmployeesType.Deliver,
                                AccountMock.GetDefault(), new List<Phone> { PhoneMock.GetDefault() }, null, DataTest.CPF, DateTime.Now)
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
        public void ShouldNotUpdateEmailCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedEmail = "teste";

            var ex = Assert.Throws<UserException>(() =>
            //act
                employee.UpdateEmail(expectedEmail)
            );

            //assert
            Assert.Equal("E-mail não é válido!", ex.Message);
        }

        [Fact]
        public void ShouldUpdatePasswordCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedPassword = DataTest.PASSWORD + "Teste";

            //act
            employee.UpdatePassword(expectedPassword);

            //assert
            Assert.Equal(expectedPassword, employee.Password);
        }

        [Fact]
        public void ShouldNotUpdatePasswordCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedPassword = "Teste";

            var ex = Assert.Throws<UserException>(() =>
                //act
                employee.UpdatePassword(expectedPassword)
            );

            //assert
            Assert.Equal("Senha inválida!", ex.Message);
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
            var expDocument = "713.385.260-81";

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

        [Fact]
        public void ShouldUpdatePasswordHashCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedPasswordHash = "Restarante@gdgdagkag.com";

            //act
            employee.UpdatePassword("Restarante@123", expectedPasswordHash);

            //assert
            Assert.Equal(expectedPasswordHash, employee.Password);
        }

        [Fact]
        public void ShouldNotUpdatePasswordHashCorrectly()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();
            var expectedPasswordHash = "Restarante@gdgdagkag.com";

            //act
            employee.UpdatePassword(employee.Password, expectedPasswordHash);

            //assert
            Assert.Equal(employee.Password, employee.Password);
        }

        [Fact]
        public void ShouldHidePassword()
        {
            //arrange
            var employee = EmployeeMock.GetDefault();

            //act
            employee.HidePassword();

            //assert
            Assert.Equal(string.Empty, employee.Password);
        }
    }
}
