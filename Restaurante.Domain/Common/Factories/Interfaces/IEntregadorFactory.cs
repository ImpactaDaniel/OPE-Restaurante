using Restaurante.Domain.Users.Entregadores.Models;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IEntregadorFactory : IFactory<Entregador>, IUserFactory<Entregador>
    {
        IEntregadorFactory WithVehicle(Veiculo veiculo);
    }
}
