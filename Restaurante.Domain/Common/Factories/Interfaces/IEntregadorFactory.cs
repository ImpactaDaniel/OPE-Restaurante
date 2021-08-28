using Restaurante.Domain.Users.Entregadores;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IEntregadorFactory : IFactory<Entregador>, IUserFactory<Entregador>
    {
        IEntregadorFactory WithVehicle(Veiculo veiculo);
    }
}
