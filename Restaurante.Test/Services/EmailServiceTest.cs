using Restaurante.Domain.Common.Models;
using Restaurante.Infra.Common.Services;
using Restaurante.Infra.Common.Settings;
using Restaurante.Test.Services.Mock;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class EmailServiceTest
    {
        
        [Fact]
        public async Task ShouldSendEmail()
        {
            //Given a valid email
            var settings = EmailSettingsMock.GetEmailTestSettings();
            var message = new Message(settings.SendTo, "Test", "teste 12346");
            var emailService = new SmtpEmailSenderService(new SmtpEmailSettings(settings.Email, settings.Password));
            
            //When sending email
            var res = await emailService.SendAsync(message);

            //Should send
            Assert.True(res.Success);
        }
    }
}
