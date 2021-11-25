using Restaurante.Domain.Integrations.EventBus.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Integrations.Interface
{
    public interface IEventBus
    {
        Task PublishAsync(string queueName, IntegrationEvent @event, CancellationToken cancellationToken = default);
    }
}
