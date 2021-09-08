using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Users.Entregadores.Models;

namespace Restaurante.Application.Common.Data.Mappers
{
    public class EntregadorIntegrationMapper : IMapper<Deliver, EntregadorIntegration>
    {
        public EntregadorIntegration Map(Deliver source, EntregadorIntegration dest = null) =>
            new EntregadorIntegration();
    }
}
