using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Employees.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Customers.Models
{
    public class Customer : User
    {
        public IList<Address> Addresses { get; set; }
    }
}
