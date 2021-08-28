using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Enums;
using System;

namespace Restaurante.Domain.Users.Funcionarios
{
    public class Funcionario : Entity<int>
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public TiposFuncionario Type { get; private set; }

        protected Funcionario()
        {
        }

        public Funcionario(string name, string email, string password, TiposFuncionario type)
        {
            ValidateNullString(name, "Nome");
            ValidateNullString(email, "E-mail");
            Name = name;
            Email = email;
            Password = password;
            Type = type;
        }

        public Funcionario UpdateName(string name)
        {
            ValidateNullString(name, "Nome");
            if (name != Name)
                Name = name;
            return this;
        }
        public Funcionario UpdateEmail(string email)
        {
            ValidateNullString(email, "E-mail");
            if (email != Email)
                Email = email;
            return this;
        }
        public Funcionario UpdateType(TiposFuncionario type)
        {
            if (type == TiposFuncionario.Entregador)
                throw new ArgumentException("Esse funcionário não pode ser entregador!");
            Type = type;
            return this;
        }
    }
}
