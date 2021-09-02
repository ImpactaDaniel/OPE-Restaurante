﻿using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using Restaurante.Domain.Users.Funcionarios.Models;
using System;

namespace Restaurante.Domain.Common.Factories
{
    internal class FuncionarioFactory : UserFactory<Funcionario>, IFuncionarioFactory
    {
        protected TiposFuncionario _type;
        public Funcionario Build()
        {
            if (string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_name))
                throw new UserException("Nome, e-mail e senha precisam estar preenchidos!");
            return new Funcionario(_name, _email, _password, _type);
        }
        public virtual IFuncionarioFactory WithType(TiposFuncionario type)
        {
            _type = type == TiposFuncionario.Entregador ? throw new UserException("Esse funcionário não pode ser do tipo entregador!") : type;
            return this;
        }
    }
}
