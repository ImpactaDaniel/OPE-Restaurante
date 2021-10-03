﻿using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Helpers;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Exceptions;
using System;

namespace Restaurante.Domain.Users.Common.Models
{
    public abstract class User : Entity<int>
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public bool FirstAccess { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; protected set; }
        public EmployeesType Type { get; protected set; }
        protected User()
        {
        }
        public User(string name, string email, string password, EmployeesType type)
            : this()
        {
            ValidateNullString(name, "Nome");
            ValidateEmail(email);
            Name = name;
            Email = email;
            Password = password;
            Type = type;
        }

        public virtual User UpdateName(string name)
        {
            ValidateNullString(name, "Nome");
            if (name != Name)
                Name = name;
            return this;
        }
        public virtual User UpdateEmail(string email)
        {
            ValidateEmail(email);
            if (email != Email)
                Email = email;
            return this;
        }

        public virtual User UpdatePassword(string password)
        {
            ValidateNullString(password, "Senha");
            if (password != Password)
                Password = password;
            return this;
        }

        private void ValidateEmail(string email)
        {
            ValidateNullString(email, "E-mail");
            if (!email.ValidEmail())
                throw new UserException("E-mail não é válido!", NotificationKeys.InvalidEmail);
        }
    }
}
