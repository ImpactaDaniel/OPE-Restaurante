using Restaurante.Domain.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface IMessageSenderService<TMessage>
        where TMessage : Message
    {
        Task<SenderResponse> SendAsync(TMessage message, CancellationToken cancellationToken = default); 
    }
}
