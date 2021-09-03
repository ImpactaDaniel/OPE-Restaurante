using Restaurante.Domain.Common.Models.Integration;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface IEntregadorIntegrationService
    {
        Task<IntegrationResponse> CreateNewEntregador(EntregadorIntegration entregador, CancellationToken cancellationToken = default);
        Task<IList<EntregadorIntegration>> GetAvailables(CancellationToken cancellationToken = default);
    }
}
