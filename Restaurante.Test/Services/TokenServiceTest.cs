using Restaurante.Application.Users.Common.Services;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using Restaurante.Test.Usuarios.Mocks;
using System;
using Xunit;

namespace Restaurante.Test.Services
{
    public class TokenServiceTest
    {
        private readonly ITokenService _tokenService;
        public TokenServiceTest()
        {
            var tokenConfiguration = new TokenConfiguration("TH1S 1S 4 V3RY S3CR3T K3Y", 2);
            _tokenService = new TokenService(tokenConfiguration);
        }
        [Fact]
        public void ShouldGenerateToken()
        {
            //Arrange
            var user = EmployeeMock.GetDefault();

            //Act
            var tokenResponse = _tokenService.GenerateToken(user);

            //Assert
            Assert.NotNull(tokenResponse.Token);
        }
    }
}
