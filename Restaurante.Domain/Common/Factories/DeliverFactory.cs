﻿using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Exceptions;
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
        public DeliveryPerson Build()
        {
            if (_veiculo is null)
                throw new UserException("Entregador precisa ter um veículo!");
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name) || !_phones.Any() || _account is null || _address is null)
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!");
            return new DeliveryPerson(_name, _email, _password, _veiculo, _account, _phones, _address);
        }

        public IDeliverFactory WithAccount(Account account)
        {
            _account = account ?? throw new UserException("Conta não pode ser nula!");
            return this;
        }

        public IDeliverFactory WithAddress(Address address)
        {
            _address = address ?? throw new UserException("Endereço não pode ser nulo!");
            return this;
        }

        public IDeliverFactory WithPhone(Phone phone)
        {
            _phones.Add(phone ?? throw new UserException("Telefone não pode ser nulo!"));
            return this;
        }

        public IDeliverFactory WithPhones(IEnumerable<Phone> phones)
        {
            if (phones.Any())
            {
                foreach (var phone in phones)
                    _phones.Add(phone ?? throw new UserException("Telefone não pode ser nulo!"));
            }
            return this;
        }

        public IDeliverFactory WithVehicle(Vehicle veiculo)
        {
            _veiculo = veiculo ?? throw new UserException("Veículo inválido!");
            return this;
        }
    }
}
