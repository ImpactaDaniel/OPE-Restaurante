using Restaurante.Domain.Users.Common.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Customers.Models
{
    public class Customer : User
    {
        public string Document { get; set; }
        public Customer(string name, string email, string password, string document)
        {
            Email = email;
            Password = password;
            Name = name;
            Document = document;
        }
        public IList<CustomerAddress> Addresses { get; set; }
    }
}
