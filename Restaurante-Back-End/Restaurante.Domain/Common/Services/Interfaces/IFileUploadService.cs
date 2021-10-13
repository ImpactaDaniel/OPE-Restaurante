using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface IFileUploadService
    {
        Task SaveFile(byte[] fileArray, string fileName, CancellationToken cancellationToken = default);
    }
}
