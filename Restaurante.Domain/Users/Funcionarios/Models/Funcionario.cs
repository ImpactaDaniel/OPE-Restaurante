﻿using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using System;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Funcionarios.Models
{
    public class Funcionario : Entity<int>
    {        
        public string Name { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public IList<Phone> Telefones { get; private set; }
        public Address Address { get; private set; }
        public Account Account { get; private set; }        
        public string Email { get; private set; }
        public string Password { get; private set; }
        public TiposFuncionario Type { get; private set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; private set; }

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
                throw new UserException("Esse funcionário não pode ser entregador!");
            Type = type;
            return this;
        }
    }
}