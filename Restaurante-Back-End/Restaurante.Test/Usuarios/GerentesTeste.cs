using NSubstitute;
using System.Net.Mail;
using Xunit;

namespace Restaurante.Test.Usuarios
{
    public class GerentesTeste
    {
       
        [Fact]
        public void DeveraEnviarEmailQuandoCadastrarNovoUsuario()
        {
            //Arrange
            //var emailService = Substitute.For<IEmailService>();           
            var smtp = new SmtpClient();

            //var usuarioParaCadastrar = usuario.

            //Act

            //Gerente cadastra

            //Assert
            //emailService.Receveid<>(string, string);
        }
    }
}
