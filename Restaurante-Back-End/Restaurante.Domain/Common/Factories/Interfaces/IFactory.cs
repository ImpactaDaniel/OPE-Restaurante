namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IFactory<T>
    {
        T Build();
    }
}
