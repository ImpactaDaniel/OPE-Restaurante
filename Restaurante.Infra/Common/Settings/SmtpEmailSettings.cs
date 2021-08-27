namespace Restaurante.Infra.Common.Settings
{
    internal class SmtpEmailSettings
    {
        public string Email { get; }
        public string Password { get; }
        public SmtpEmailSettings(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
