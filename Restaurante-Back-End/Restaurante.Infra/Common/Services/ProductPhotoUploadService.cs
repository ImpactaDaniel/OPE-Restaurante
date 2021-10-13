using Restaurante.Domain.Common.Services.Interfaces;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Infra.Common.Services
{
    internal class ProductPhotoUploadService : IProductPhotoUploadService
    {
        public async Task SaveFile(byte[] fileArray, string fileName, CancellationToken cancellationToken = default) =>
            await File.WriteAllBytesAsync(fileName, fileArray, cancellationToken);

        public async Task<string> SaveProductPhoto(string base64File, string fileName, CancellationToken cancellationToken = default)
        {
            fileName = Guid.NewGuid() + fileName;
            var photoDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\Product\\Photos";
            if (!Directory.Exists(photoDirectory))
                Directory.CreateDirectory(photoDirectory);

            var photoPath = $"{photoDirectory}\\{fileName}";

            await SaveFile(Convert.FromBase64String(base64File), photoPath, cancellationToken);

            return fileName;
        }
    }
}
