using FizzWare.NBuilder;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using System.Collections.Generic;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class EmployeeMock
    {
        public static Employee GetDefaultManager() =>
            Builder<Employee>
            .CreateNew()
            .WithFactory(() => new Employee("Carlos", "carlos@gmail.com", "123456", EmployeesType.Manager, new Account(new Bank("'"), "", "", 0), new List<Phone>(), new Address("", "", "", "")))
            .Build();

        public static Employee GetDefault() =>
            Builder<Employee>
            .CreateNew()
            .WithFactory(() => new Employee("Carlos", "carlos@gmail.com", "123456", EmployeesType.Employee, new Account(new Bank("'"), "", "", 0), new List<Phone>(), new Address("", "", "", "")))
            .Build();
    }
}
