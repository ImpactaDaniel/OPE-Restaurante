using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Funcionarios.Models
{
    public class Funcionario : User
    {
        public Account Account { get; private set; }
        public IList<Phone> Phones { get; private set; }
        public Address Address { get; set; }
        protected Funcionario()
        {
        }
        public Funcionario(string name, string email, string password, TiposFuncionario type, Account account, IList<Phone> phones, Address address) :
            base(name, email, password, type)
        {
            Account = account;
            Phones = phones;
            Address = address;
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
