using FizzWare.NBuilder;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using System;
using System.Collections.Generic;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class EmployeeMock
    {
        public static Employee GetDefaultManager() =>
            Builder<Employee>
            .CreateNew()
            .WithFactory(() => new Employee("Carlos", "carlos@gmail.com", DataTest.PASSWORD, UsersType.Manager, AccountMock.GetDefault(), new List<Phone>() { PhoneMock.GetDefault() }, AddressMock.GetDefault(), DataTest.CPF, DateTime.Now))
            .Build();

        public static Employee GetDefault() =>
            Builder<Employee>
            .CreateNew()
            .WithFactory(() => new Employee("Carlos", "carlos@gmail.com", DataTest.PASSWORD, UsersType.Employee, AccountMock.GetDefault(), new List<Phone>() { PhoneMock.GetDefault() }, AddressMock.GetDefault(), DataTest.CPF, DateTime.Now))
            .Build();
    }

    public static class BankMock
    {
        public static Bank GetDefault() =>
            Builder<Bank>
            .CreateNew()
            .WithFactory(() => new Bank("Teste"))
            .Build();
    }

    public static class AccountMock
    {
        public static Account GetDefault() =>
            Builder<Account>
            .CreateNew()
            .WithFactory(() => new Account(BankMock.GetDefault(), "teste", "teste", 0))
            .Build();
    }
    public static class PhoneMock
    {
        public static Phone GetDefault() =>
            Builder<Phone>
            .CreateNew()
            .WithFactory(() => new Phone("11", "00000000"))
            .Build();
    }
    public static class AddressMock
    {
        public static Address GetDefault() =>
            Builder<Address>
            .CreateNew()
            .WithFactory(() => new Address("teste", "teste", "teste", "00000000", "SP", "São Paulo"))
            .Build();
    }
}
