using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Factories.Interfaces;
using Restaurante.Domain.Users.Exceptions;
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
            _email = string.IsNullOrEmpty(email) ? throw new UserException("E-mail precisa estar preenchido!", NotificationKeys.InvalidEntity) : email;
            return this;
        }

        public virtual IUserFactory<T> WithName(string name)
        {
            _name = string.IsNullOrEmpty(name) ? throw new UserException("Nome precisa estar preenchido!", NotificationKeys.InvalidEntity) : name;
            return this;
        }

        public virtual IUserFactory<T> WithPassword(string password)
        {
            _password = string.IsNullOrEmpty(password) ? throw new UserException("Senha precisa estar preenchido!", NotificationKeys.InvalidEntity) : password;
            return this;
        }
    }
}
