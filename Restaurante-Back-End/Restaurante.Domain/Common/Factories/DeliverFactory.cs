using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurante.Domain.Common.Factories
{
    internal class DeliverFactory : UserFactory<DeliveryPerson>, IDeliverFactory
    {
        private Vehicle _veiculo;
        protected Account _account;
        protected Address _address;
        protected IList<Phone> _phones = new List<Phone>();
        protected string _document;
        protected DateTime _birthDate;
        public DeliveryPerson Build()
        {
            if (_veiculo is null)
                throw new UserException("Entregador precisa ter um veículo!", NotificationKeys.InvalidEntity);
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name) || !_phones.Any() || _account is null || _address is null)
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!", NotificationKeys.InvalidEntity);
            return new DeliveryPerson(_name, _email, _password, _veiculo, _account, _phones, _address, _document, _birthDate);
        }

        public IDeliverFactory WithAccount(Account account)
        {
            _account = account ?? throw new UserException("Conta não pode ser nula!", NotificationKeys.InvalidEntity);
            return this;
        }

        public IDeliverFactory WithAddress(Address address)
        {
            _address = address ?? throw new UserException("Endereço não pode ser nulo!", NotificationKeys.InvalidEntity);
            return this;
        }

        public IDeliverFactory WithPhone(Phone phone)
        {
            _phones.Add(phone ?? throw new UserException("Telefone não pode ser nulo!", NotificationKeys.InvalidEntity));
            return this;
        }

        public IDeliverFactory WithPhones(IEnumerable<Phone> phones)
        {
            if (phones.Any())
            {
                foreach (var phone in phones)
                    _phones.Add(phone ?? throw new UserException("Telefone não pode ser nulo!", NotificationKeys.InvalidEntity));
            }
            return this;
        }

        public IDeliverFactory WithVehicle(Vehicle veiculo)
        {
            _veiculo = veiculo ?? throw new UserException("Veículo inválido!", NotificationKeys.InvalidEntity);
            return this;
        }

        public IDeliverFactory WithBirthDate(DateTime birthDate)
        {
            _birthDate = birthDate;
            return this;
        }

        public IDeliverFactory WithDocument(string document)
        {
            _document = string.IsNullOrEmpty(document) ? throw new UserException("CPF não pode ser vazio!", NotificationKeys.InvalidEntity) : document;
            return this;
        }
    }
}
