using Restaurante.Domain.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface IMessageSenderService
    {
        Task<SenderResponse> SendAsync(Message message, CancellationToken cancellationToken = default); 
    }
}
