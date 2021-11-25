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


            var customer = new Customer(1, "Daniel", "teste@gmail.com", "$2b$10$JslArELBa5XKc8b8crVZ9.GN1bY90Cf7c5ibgoTj.vO3fL7pt863q", "10845441051");

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
