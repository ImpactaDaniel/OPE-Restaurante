using Restaurante.Domain.Common.Data.Mappers.Interfaces;
using System;
using System.Collections.Generic;

namespace Restaurante.Application.Common.Data.Mappers
{
    public abstract class Mapper<TSource, TDest> : IMapper<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        public abstract TDest Map(TSource source);

        public IEnumerable<TDest> Map(IEnumerable<TSource> sources)
        {
            foreach (var source in sources)
                yield return Map(source);
        }
    }
}
