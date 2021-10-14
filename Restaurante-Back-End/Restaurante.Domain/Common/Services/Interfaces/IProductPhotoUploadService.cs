using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface IProductPhotoUploadService : IFileUploadService
    {
        Task<string> SaveProductPhoto(string base64File, string fileName, CancellationToken cancellationToken = default);
    }
}
