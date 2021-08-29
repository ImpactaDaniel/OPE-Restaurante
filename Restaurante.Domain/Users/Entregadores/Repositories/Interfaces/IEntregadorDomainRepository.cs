using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Funcionarios.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Entregadores.Repositories
{
    public interface IEntregadorDomainRepository : IFuncionarioDomainRepository<Entregador>
    {
        Task<Veiculo> GetVehicle(int id, CancellationToken cancellationToken = default);
    }
}
