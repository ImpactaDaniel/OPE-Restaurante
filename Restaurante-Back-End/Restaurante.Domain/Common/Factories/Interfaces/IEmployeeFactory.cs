using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using System;
using System.Collections.Generic;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IEmployeeFactory : IFactory<Employee>, IUserFactory<Employee>
    {
        IEmployeeFactory WithType(UsersType type);
        IEmployeeFactory WithPhone(Phone phone);
        IEmployeeFactory WithPhones(IEnumerable<Phone> phones);
        IEmployeeFactory WithAddress(Address address);
        IEmployeeFactory WithAccount(Account account);
        IEmployeeFactory WithDocument(string document);
        IEmployeeFactory WithBirthDate(DateTime birthDate);
    }
}
