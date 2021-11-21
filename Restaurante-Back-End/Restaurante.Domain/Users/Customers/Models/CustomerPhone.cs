﻿using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Customers.Models
{
    public class CustomerPhone : Entity<int>
    {
        public string DDD { get; set; }
        public string PhoneNumber { get; set; }
    }
}
