using Restaurante.Domain.Users.Common.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Customers.Models
{
    public class Customer : User
    {
        public Customer(string name, string email, string password)
        {
            Email = email;
            Password = password;
            Name = name;
        }
        public IList<CustomerAddress> Addresses { get; set; }
    }
}
