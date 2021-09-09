using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Users.Entregadores.Models;

namespace Restaurante.Application.Common.Data.Mappers
{
    public class DeliveryPersonIntegrationMapper : IMapper<DeliveryPerson, DeliveryPersonIntegration>
    {
        public DeliveryPersonIntegration Map(DeliveryPerson source, DeliveryPersonIntegration dest = null) =>
            new DeliveryPersonIntegration();
    }
}
