using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;

namespace Restaurante.Domain.Users.Funcionarios.Models
{
    public class Funcionario : User
    {
        protected Funcionario()
        {
        }
        public Funcionario(string name, string email, string password, TiposFuncionario type) :
            base(name, email, password, type)
        {
        }
        public Funcionario UpdateType(TiposFuncionario type)
        {
            if (type == TiposFuncionario.Entregador)
                throw new UserException("Esse funcionário não pode ser entregador!");
            Type = type;
            return this;
        }
    }
}
