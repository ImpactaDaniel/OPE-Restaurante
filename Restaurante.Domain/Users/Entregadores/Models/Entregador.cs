using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Models;

namespace Restaurante.Domain.Users.Entregadores.Models
{
    public class Entregador : Funcionario
    {
        public Veiculo Moto { get; private set; }

        private Entregador()
        {
        }
        public Entregador(string name, string email, string password, Veiculo moto) 
            : base(name, email, password, TiposFuncionario.Entregador)
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
