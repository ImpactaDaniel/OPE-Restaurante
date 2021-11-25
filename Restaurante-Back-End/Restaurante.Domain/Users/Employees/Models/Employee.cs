using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Helpers;
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
        public DateTime BirthDate { get; private set; }
        public string Document { get; private set; }
        public Address Address { get; set; }
        protected Employee()
        {
        }
        public Employee(string name, string email, string password, UsersType type, Account account, IList<Phone> phones, Address address, string document, DateTime birthDate) :
            base(name, email, password, type)
        {
            ValidateDocument(document);
            Account = account ?? throw new UserException("Conta não pode ser nula!", NotificationKeys.InvalidEntity);
            Phones = (phones is null || phones.Count <= 0) ? throw new UserException("Telefones não podem ser nulos!", NotificationKeys.InvalidEntity) : phones;
            Address = address ?? throw new UserException("Endereço não pode ser nulo!", NotificationKeys.InvalidEntity);
            BirthDate = birthDate;
            Document = document;
        }

        public Employee(int id, string name, string email, string password, UsersType type, Account account, IList<Phone> phones, Address address, string document, DateTime birthDate) :
            base(id, name, email, password, type)
        {
            ValidateDocument(document);
            Account = account ?? throw new UserException("Conta não pode ser nula!", NotificationKeys.InvalidEntity);
            Phones = (phones is null || phones.Count <= 0) ? throw new UserException("Telefones não podem ser nulos!", NotificationKeys.InvalidEntity) : phones;
            Address = address ?? throw new UserException("Endereço não pode ser nulo!", NotificationKeys.InvalidEntity);
            BirthDate = birthDate;
            Document = document;
        }

        public Employee UpdateType(UsersType type)
        {
            if (type == UsersType.Deliver)
                throw new UserException("Esse funcionário não pode ser entregador!", NotificationKeys.InvalidEntity);
            Type = type;
            return this;
        }

        public Employee UpdateAccount(Account account)
        {
            account = account ?? throw new UserException("Conta não pode ser nula!", NotificationKeys.InvalidEntity);
            if (Account.Id != account.Id)
                Account = account;
                Account = account;
            return this;
        }

        public Employee UpdateBirthDate(DateTime birthDate)
        {
            BirthDate = birthDate;
            return this;
        }

        public Employee UpdateDocument(string document)
        {
            ValidateDocument(document);
            if (Document != document)
                Document = document;
            return this;
        }

        private void ValidateDocument(string document)
        {
            ValidateNullString(document, "CPF");
            if (!document.ValidCPF())
                throw new UserException("CPF não é válido!", NotificationKeys.InvalidEntity);
        }
    }
}
