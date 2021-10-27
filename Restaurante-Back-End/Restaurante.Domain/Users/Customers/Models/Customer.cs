using Restaurante.Domain.Users.Common.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Customers.Models
{
    public class Customer : User
    {
        public IList<CustomerAddress> Addresses { get; set; }
    }
}
