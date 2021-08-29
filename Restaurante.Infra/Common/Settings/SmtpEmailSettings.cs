namespace Restaurante.Infra.Common.Settings
{
    internal class SmtpEmailSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public SmtpEmailSettings()
        {

        }
        public SmtpEmailSettings(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
