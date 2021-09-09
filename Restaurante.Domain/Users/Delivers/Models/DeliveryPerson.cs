using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;
using System.Collections.Generic;

namespace Restaurante.Domain.Users.Entregadores.Models
{
    public class DeliveryPerson : Employee
    {
        public Vehicle MotoCycle { get; private set; }

        private DeliveryPerson()
        {
        }
        public DeliveryPerson(string name, string email, string password, Vehicle moto, Account account, IList<Phone> phones, Address address)
            : base(name, email, password, EmployeesType.Deliver, account, phones, address)
        {
            MotoCycle = moto;
        }

        public DeliveryPerson UpdateVehicle(Vehicle veiculo)
        {
            MotoCycle = veiculo;
            return this;
        }
    }
}
