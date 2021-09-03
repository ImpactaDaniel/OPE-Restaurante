using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Entregadores.Models
{
    public class Entregador : Funcionario
    {
        public Veiculo Moto { get; private set; }

        private Entregador()
        {
        }
        public Entregador(string name, string email, string password, Veiculo moto, Account account, IList<Phone> phones, Address address)
            : base(name, email, password, TiposFuncionario.Entregador, account, phones, address)
        {
            Moto = moto;
        }

        public Entregador UpdateVehicle(Veiculo veiculo)
        {
            Moto = veiculo;
            return this;
        }
    }
}
