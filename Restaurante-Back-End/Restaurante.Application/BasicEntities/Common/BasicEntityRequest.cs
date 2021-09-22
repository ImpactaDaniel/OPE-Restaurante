using Restaurante.Application.Common;
using Restaurante.Domain.BasicEntities.Common.Interfaces;

namespace Restaurante.Application.BasicEntities.Common
{
    public abstract class BasicEntityRequest<TEntity, TRequest, TId> : EntityRequest<TId>
        where TRequest : EntityRequest<int>
        where TEntity : class, IBasicEntity
    {
        public TEntity Entity { get; set; }
    }
}
