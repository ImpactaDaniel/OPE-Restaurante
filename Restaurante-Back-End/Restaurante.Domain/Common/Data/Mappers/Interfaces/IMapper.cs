namespace Restaurante.Domain.Common.Data.Mappers.Interfaces
{
    public interface IMapper<TSource, TDest>
        where TSource : class
        where TDest : class
    {
        TDest Map(TSource source, TDest dest = null);
    }
}
