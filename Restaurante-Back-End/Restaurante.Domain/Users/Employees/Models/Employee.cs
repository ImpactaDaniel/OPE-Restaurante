using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using System;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Employees.Models
{
    public class Employee : User
    {
        public Account Account { get; private set; }
        public IList<Phone> Phones { get; private set; }
        public Address Address { get; set; }
        protected Employee()
        {
        }
        public Employee(string name, string email, string password, EmployeesType type, Account account, IList<Phone> phones, Address address) :
            base(name, email, password, type)
        {
            Account = account ?? throw new ArgumentNullException(nameof(account));
            Phones = phones ?? throw new ArgumentNullException(nameof(phones));
            Address = address ?? throw new ArgumentNullException(nameof(address));
        }
        public Employee UpdateType(EmployeesType type)
        {
            if (type == EmployeesType.Deliver)
                throw new UserException("Esse funcionário não pode ser entregador!");
            Type = type;
            return this;
        }

        public Employee UpdateAccount(Account account)
        {
            if (Account != account)
                Account = account;
            return this;
        }

    }
}
