using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Customers.Models;
using System.Collections.Generic;

namespace Restaurante.Infra.Seeds
{
    internal static partial class Seeds
    {
        public static void CustomersSeeds(this ModelBuilder builder)
        {
            var phone = new CustomerPhone(1)
            {
                DDD = "11",
                PhoneNumber = "910703000"
            };

            builder.Entity<CustomerPhone>().HasData(phone);


            var customer = new Customer(1, "Daniel", "daniel@gmail.com", "$2b$10$9QsGNTO4SNA6QqsrQRq/AutnF9I3XQLQYKv6ofHvwpuyb0.w97bZa", "10845441051");

            customer.PhoneId = 1;

            builder.Entity<Customer>().HasData(customer);

            var address = new CustomerAddress(1, "Rua Ângelo Benincori", "180", "City Jaraguá", "02998190", "SP", "São Paulo")
            {
                CustomerId = 1
            };

            builder.Entity<CustomerAddress>().HasData(address);
        }
    }
}
