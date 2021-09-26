using Restaurante.Domain.Common.Factories;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Test.Usuarios.Mocks;
using System;
using System.Collections.Generic;
using Xunit;

namespace Restaurante.Test.Usuarios
{
    public class EmployeeFactoryTest
    {
        private readonly IEmployeeFactory _factory;

        public EmployeeFactoryTest()
        {
            _factory = new EmployeeFactory();
        }

        [Fact]
        public void ShouldCreateNewDeliveryManWhenValidInfos()
        {
            //Arrange
            _factory
                .WithType(Restaurante.Domain.Users.Enums.EmployeesType.Manager)
                .WithDocument("45464")
                .WithBirthDate(DateTime.Now)
                .WithAccount(AccountMock.GetDefault())
                .WithAddress(AddressMock.GetDefault())
                .WithPhone(PhoneMock.GetDefault())
                .WithName("Daniel")
                .WithEmail("daniel@gmail.com")
                .WithPassword("123456");

            //Act
            var employee = _factory.Build();

            //Assert
            Assert.NotNull(employee);
            Assert.Equal("Daniel", employee.Name);
        }


        [Fact]
        public void ShouldCreteNewDeliverWhenItsValid()
        {
            //Arrange
            _factory
                .WithAccount(AccountMock.GetDefault())
                .WithDocument("45464")
                .WithBirthDate(DateTime.Now)
                .WithType(Restaurante.Domain.Users.Enums.EmployeesType.Employee)
                .WithAddress(AddressMock.GetDefault())
                .WithPhones(new List<Phone> { PhoneMock.GetDefault() })
                .WithName("Daniel")
                .WithEmail("daniel@gmail.com")
                .WithPassword("123456");

            //Act
            var employee = _factory.Build();

            //Assert
            Assert.NotNull(employee);
            Assert.Equal("Daniel", employee.Name);
        }

        [Fact]
        public void ShouldNotCreateAnDeliveryMan()
        {
            //assert
            Assert.Throws<UserException>(() =>
                //act
                _factory
                    .WithAccount(AccountMock.GetDefault())
                    .WithDocument("45464")
                    .WithBirthDate(DateTime.Now)
                    .WithType(Restaurante.Domain.Users.Enums.EmployeesType.Deliver)
                    .WithAddress(AddressMock.GetDefault())
                    .WithPhones(new List<Phone> { PhoneMock.GetDefault() })
                    .WithName("Daniel")
                    .WithEmail("daniel@gmail.com")
                    .WithPassword("123456")

            );
        }
    }
}
