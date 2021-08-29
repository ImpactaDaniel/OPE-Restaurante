using Restaurante.Domain.Common.Models.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Common.Services.Interfaces
{
    public interface IEntityService<TEntity>
        where TEntity : IEntity
    {
        Task<TEntity> Get(int id, CancellationToken cancellationToken = default);
    }
}
