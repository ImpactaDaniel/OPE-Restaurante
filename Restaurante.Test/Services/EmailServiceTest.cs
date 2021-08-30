using NSubstitute;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Common.Services.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Restaurante.Test.Services
{
    public class EmailServiceTest
    {
        IMessageSenderService _emailService;
        public EmailServiceTest()
        {
            _emailService = Substitute.For<IMessageSenderService>();
            _emailService.SendAsync(default).ReturnsForAnyArgs(new SenderResponse(true, string.Empty));
        }

        [Fact]
        public async Task ShouldSendEmail()
        {
            //Given a valid email
            var message = new Message(string.Empty, "Test", "teste 12346");

            //When sending email
            var res = await _emailService.SendAsync(message);

            //Should send
            Assert.True(res.Success);
        }
    }
}
