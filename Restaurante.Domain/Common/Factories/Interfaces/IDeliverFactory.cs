using Restaurante.Domain.Users.Entregadores.Models;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IDeliverFactory : IFactory<Deliver>, IUserFactory<Deliver>
    {
        IDeliverFactory WithVehicle(Vehicle veiculo);
    }
}
