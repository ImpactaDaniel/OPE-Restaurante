using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Employees.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurante.Infra.Common.Persistence
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly RestauranteDbContext _db;
        private readonly IEmployeeDomainRepository<Employee> _repository;

        public DatabaseInitializer(RestauranteDbContext db, IEmployeeDomainRepository<Employee> repository)
        {
            _db = db;
            _repository = repository;
        }

        public void Initialize()
        {
            _db.Database.EnsureCreated();

            if(!_db.Employees.Any())
                _repository.CreateEmployee(new Employee("Admin", "admin@admin.com", "12346", Domain.Users.Enums.EmployeesType.Manager, new Account(new Bank("Teste"), "teste", "teste", 1),
                    new List<Phone> { new Phone("11", "000000000") }, new Address("Angelo", "112", "Pirituba", "00000000", "SP", "são paulo"), "121.085.900-99", DateTime.Now));
        }
    }
}
