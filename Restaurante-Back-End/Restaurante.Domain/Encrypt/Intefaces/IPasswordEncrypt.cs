namespace Restaurante.Domain.Encrypt.Intefaces
{
    public interface IPasswordEncrypt : IEncrypt
    {
        bool Compare(string encryptedValue, string value);
    }
}
