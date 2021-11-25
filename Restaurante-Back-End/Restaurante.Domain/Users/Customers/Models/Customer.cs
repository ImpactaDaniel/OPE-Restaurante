using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Employees.Models;
using System;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Customers.Models
{
    public class Customer : User
    {
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public Customer(string name, string email, string password, string document) :
            base(name, email, password, Enums.UsersType.Customer)
        {
            Email = email;
            Password = password;
            Name = name;
            Document = document;
        }

        public Customer(int id, string name, string email, string password, string document)
            : base(id, name, email, password, Enums.UsersType.Customer)
        {
            Document = document;
        }
        public IList<CustomerAddress> Addresses { get; set; }
        public CustomerPhone Phone { get; set; }
        public int PhoneId { get; set; }
    }
}
