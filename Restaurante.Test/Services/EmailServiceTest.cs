using Restaurante.Domain.Common.Models;
using Restaurante.Infra.Common.Services;
using Restaurante.Infra.Common.Settings;
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
            var settings = new SmtpEmailSettings("noreply.restaurante.mataburro@gmail.com", "TCC!mp4ct4");
            var emailService = new SmtpEmailSenderService(settings);
            var validEmail = "danielcity1@gmail.com";
            var message = new Message(validEmail, "Test", "Test send email");

            //When sending email
            var res = await emailService.SendAsync(message);

            //Should send
            Assert.True(res.Success);
        }
    }
}
