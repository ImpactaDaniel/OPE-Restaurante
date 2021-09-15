using Microsoft.Extensions.Configuration;
using System.IO;

namespace Restaurante.Test.Services.Mock
{
    public static class EmailSettingsMock
    {
        internal static EmailTestSettings GetEmailTestSettings()
        {
            if (File.Exists("test.settings.local.json"))
                return new ConfigurationBuilder()
                    .AddJsonFile("test.settings.local.json")
                    .Build()
                    .GetSection(nameof(EmailTestSettings))
                    .Get<EmailTestSettings>();

            return new ConfigurationBuilder()
                    .AddJsonFile("test.settings.json")
                    .Build()
                    .GetSection(nameof(EmailTestSettings))
                    .Get<EmailTestSettings>();
        }
    }
    internal class EmailTestSettings
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SendTo { get; set; }
    }
}
