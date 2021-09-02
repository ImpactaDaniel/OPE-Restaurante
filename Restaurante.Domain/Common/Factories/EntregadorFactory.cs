using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Exceptions;
using System;

namespace Restaurante.Domain.Common.Factories
{
    internal class EntregadorFactory : UserFactory<Entregador>, IEntregadorFactory
    {
        private Veiculo _veiculo;
        public Entregador Build()
        {
            if (_veiculo is null)
                throw new UserException("Entregador precisa ter um veículo!");
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name))
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!");
            return new Entregador(_name, _email, _password, _veiculo);
        }

        public IEntregadorFactory WithVehicle(Veiculo veiculo)
        {
            _veiculo = veiculo ?? throw new UserException("Veículo inválido!");
            return this;
        }
    }
}
