using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Exceptions;
using System.Collections.Generic;

namespace Restaurante.Domain.Common.Factories
{
    internal class DeliverFactory : UserFactory<Deliver>, IDeliverFactory
    {
        private Vehicle _veiculo;
        public Deliver Build()
        {
            if (_veiculo is null)
                throw new UserException("Entregador precisa ter um veículo!");
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name))
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!");
            return new Deliver(_name, _email, _password, _veiculo, new Account(new Bank(""), "", "", 0), new List<Phone>(), new Address("", "", "", ""));
        }

        public IDeliverFactory WithVehicle(Vehicle veiculo)
        {
            _veiculo = veiculo ?? throw new UserException("Veículo inválido!");
            return this;
        }
    }
}
