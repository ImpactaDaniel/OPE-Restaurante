using Restaurante.Domain.Encrypt.Intefaces;

namespace Restaurante.Infra.Encrypt
{
    internal class PasswordEncrypt : IPasswordEncrypt
    {
        public bool Compare(string encryptedValue, string value) => 
            BCrypt.Net.BCrypt.Verify(value, encryptedValue);

        public string Encrypt(string value) =>
            BCrypt.Net.BCrypt.HashPassword(value);
    }
}
