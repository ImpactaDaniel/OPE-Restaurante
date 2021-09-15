using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Enums;
using System;

namespace Restaurante.Domain.Users.Common.Models
{
    public abstract class User : Entity<int>
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; protected set; }
        public EmployeesType Type { get; protected set; }
        protected User()
        {
        }
        public User(string name, string email, string password, EmployeesType type)
        {
            ValidateNullString(name, "Nome");
            ValidateNullString(email, "E-mail");
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
            ValidateNullString(email, "E-mail");
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
    }
}
