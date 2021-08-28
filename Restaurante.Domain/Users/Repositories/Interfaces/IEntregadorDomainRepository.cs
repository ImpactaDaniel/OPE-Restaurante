using Restaurante.Domain.Users.Entregadores;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Repositories.Interfaces
{
    public interface IEntregadorDomainRepository : IFuncionarioDomainRepository<Entregador>
    {
        Task<Veiculo> GetVehicle(int id, CancellationToken cancellationToken = default);
    }
}
