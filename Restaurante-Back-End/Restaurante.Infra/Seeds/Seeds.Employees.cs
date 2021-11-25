using Microsoft.EntityFrameworkCore;
using Restaurante.Domain.Users.Employees.Models;
using System;
using System.Collections.Generic;

namespace Restaurante.Infra.Seeds
{
    internal partial class Seeds
    {
        public static void EmployeesSeed(this ModelBuilder builder)
        {
            var bank = new Bank("Teste", 1);

            builder.Entity<Bank>().HasData(bank);

            var account = new Account(bank, "teste", "teste", 1, 1);

            builder.Entity<Account>().HasData(account);

            var phone = new Phone(1, "11", "000000000");

            builder.Entity<Phone>().HasData(phone);

            var address = new Address(1, "Angelo", "112", "Pirituba", "00000000", "SP", "são paulo");

            builder.Entity<Address>().HasData(address);

            var employee = new Employee(1, "Admin",
                "admin@admin.com",
                "Restaurante@1234",
                Domain.Users.Enums.UsersType.Manager,
                account,
                new List<Phone> { phone },
                address,
                "121.085.900-99",
                DateTime.Now);

            builder.Entity<Employee>().HasData(employee); 
        }
    }
}
