using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using System;
using System.Collections.Generic;

namespace Restaurante.Domain.Common.Factories
{
    internal class EmployeeFactory : UserFactory<Employee>, IEmployeeFactory
    {
        protected EmployeesType _type;
        protected Account _account;
        protected Address _address;
        protected IList<Phone> _phones = new List<Phone>();
        public Employee Build()
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name))
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!");
            return new Employee(_name, _email, _password, _type, _account, _phones, _address);
        }

        public IEmployeeFactory WithAccount(Account account)
        {
            throw new NotImplementedException();
        }

        public IEmployeeFactory WithAddress(Address address)
        {
            throw new NotImplementedException();
        }

        public IEmployeeFactory WithPhone(Phone phone)
        {
            throw new NotImplementedException();
        }

        public virtual IEmployeeFactory WithType(EmployeesType type)
        {
            _type = type == EmployeesType.Deliver ? throw new UserException("Esse funcionário não pode ser do tipo entregador!") : type;
            return this;
        }
    }
}
