using Restaurante.Domain.Common.Factories.Interfaces;
using System;

namespace Restaurante.Domain.Common.Factories
{
    public abstract class UserFactory<T> : IUserFactory<T>
    {
        protected string _email;
        protected string _password;
        protected string _name;

        public virtual IUserFactory<T> WithEmail(string email)
        {
            _email = string.IsNullOrEmpty(email) ? throw new ArgumentException("E-mail precisa estar preenchido!") : email;
            return this;
        }

        public virtual IUserFactory<T> WithName(string name)
        {
            _name = string.IsNullOrEmpty(name) ? throw new ArgumentException("Nome precisa estar preenchido!") : name;
            return this;
        }

        public virtual IUserFactory<T> WithPassword(string password)
        {
            _password = string.IsNullOrEmpty(password) ? throw new ArgumentException("Senha precisa estar preenchido!") : password;
            return this;
        }
    }
}
