namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IUserFactory<T>
    {
        IUserFactory<T> WithEmail(string email);
        IUserFactory<T> WithName(string name);
        IUserFactory<T> WithPassword(string password);
    }
}
