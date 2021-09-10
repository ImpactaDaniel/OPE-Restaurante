using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using System.Collections.Generic;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IDeliverFactory : IFactory<DeliveryPerson>, IUserFactory<DeliveryPerson>
    {
        IDeliverFactory WithVehicle(Vehicle veiculo);
        IDeliverFactory WithPhone(Phone phone);
        IDeliverFactory WithPhones(IEnumerable<Phone> phones);
        IDeliverFactory WithAddress(Address address);
        IDeliverFactory WithAccount(Account account);
    }
}
