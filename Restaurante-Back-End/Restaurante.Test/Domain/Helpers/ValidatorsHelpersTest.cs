using Restaurante.Domain.Helpers;
using Xunit;

namespace Restaurante.Test.Domain.Helpers
{
    public class ValidatorsHelpersTest
    {
        [Theory]
        [InlineData("65270607000")]        
        [InlineData("799.273.500-58")]
        [InlineData("510.497.660-24")]
        [InlineData("360.997.020-08")]
        public void ShouldBeValidCPF(string cpf)
        {
            //act
            var valid = cpf.ValidCPF();

            //assert
            Assert.True(valid);
        }

        [Theory]
        [InlineData("00000000000")]
        [InlineData("652706070")]
        [InlineData("00000000001")]
        [InlineData("478.253.378-01")]
        public void ShouldBeInvalidCPF(string cpf)
        {
            //act
            var valid = cpf.ValidCPF();

            //assert
            Assert.False(valid);
        }


        [Theory]
        [InlineData("teste@teste.com")]
        [InlineData("teste@faculdade.com")]
        [InlineData("teste123@university.com")]
        [InlineData("teste@teste.com.br")]
        [InlineData("teste@t.com.br")]
        [InlineData("teste@aluno.teste.com.br")]
        public void ShouldBeValidEmail(string email)
        {
            //act
            var valid = email.ValidEmail();

            //assert
            Assert.True(valid);
        }


        [Theory]
        [InlineData("teste@.com")]
        [InlineData("teste123university.com")]
        public void ShouldBeInvalidEmail(string email)
        {
            //act
            var valid = email.ValidEmail();

            //assert
            Assert.False(valid);
        }
    }
}
