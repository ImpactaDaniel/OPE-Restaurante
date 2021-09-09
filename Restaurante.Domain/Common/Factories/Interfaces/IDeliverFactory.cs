using Restaurante.Domain.Users.Entregadores.Models;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IDeliverFactory : IFactory<DeliveryPerson>, IUserFactory<DeliveryPerson>
    {
        IDeliverFactory WithVehicle(Vehicle veiculo);
    }
}
