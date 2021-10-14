using System.Collections.Generic;

namespace Restaurante.Domain.Common.Data.Mappers.Interfaces
{
    public interface IMapper<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        TDest Map(TSource source);
        IEnumerable<TDest> Map(IEnumerable<TSource> sources);
    }
}
