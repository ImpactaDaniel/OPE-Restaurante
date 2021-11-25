using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurante.Domain.Common.Factories
{
    internal class EmployeeFactory : UserFactory<Employee>, IEmployeeFactory
    {
        protected UsersType _type;
        protected Account _account;
        protected Address _address;
        protected IList<Phone> _phones = new List<Phone>();
        protected string _document;
        protected DateTime _birthDate;
        public Employee Build()
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name) || !_phones.Any() || _account is null || _address is null || string.IsNullOrEmpty(_document))
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!", NotificationKeys.InvalidEntity);
            return new Employee(_name, _email, _password, _type, _account, _phones, _address, _document, _birthDate);
        }

        public IEmployeeFactory WithAccount(Account account)
        {
            _account = account ?? throw new UserException("Conta não pode ser nula!", NotificationKeys.InvalidEntity);
            return this;
        }

        public IEmployeeFactory WithAddress(Address address)
        {
            _address = address ?? throw new UserException("Endereço não pode ser nulo!", NotificationKeys.InvalidEntity);
            return this;
        }

        public IEmployeeFactory WithBirthDate(DateTime birthDate)
        {
            _birthDate = birthDate;
            return this;
        }

        public IEmployeeFactory WithDocument(string document)
        {
            _document = string.IsNullOrEmpty(document) ? throw new UserException("CPF não pode ser vazio!", NotificationKeys.InvalidEntity) : document;
            return this;
        }

        public IEmployeeFactory WithPhone(Phone phone)
        {
            _phones.Add(phone ?? throw new UserException("Telefone não pode ser nulo!", NotificationKeys.InvalidEntity));
            return this;
        }

        public IEmployeeFactory WithPhones(IEnumerable<Phone> phones)
        {
            if (phones.Any())
            {
                foreach (var phone in phones)
                    _phones.Add(phone ?? throw new UserException("Telefone não pode ser nulo!", NotificationKeys.InvalidEntity));
            }
            return this;
        }

        public virtual IEmployeeFactory WithType(UsersType type)
        {
            _type = type == UsersType.Deliver ? throw new UserException("Esse funcionário não pode ser do tipo entregador!", NotificationKeys.InvalidEntity) : type;
            return this;
        }
    }
}
