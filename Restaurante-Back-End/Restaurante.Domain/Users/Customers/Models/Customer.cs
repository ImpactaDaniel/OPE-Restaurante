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
        public Customer(string name, string email, string password, string document)
        {
            Email = email;
            Password = password;
            Name = name;
            Document = document;
        }
        public IList<CustomerAddress> Addresses { get; set; }
        public CustomerPhone Phone { get; set; }
    }
}
