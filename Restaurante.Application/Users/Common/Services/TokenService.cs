using Microsoft.IdentityModel.Tokens;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restaurante.Application.Users.Common.Services
{
    internal class TokenService : ITokenService
    {
        public TokenConfiguration TokenConfiguration { get; }
        public TokenService(TokenConfiguration tokenConfiguration)
        {
            TokenConfiguration = tokenConfiguration;
        }
        public TokenResponse GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenConfiguration.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Type.ToString())
                }),
                Expires = DateTime.Now.AddHours(TokenConfiguration.ValidTime),
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResponse(tokenHandler.WriteToken(token), token.ValidFrom, token.ValidTo);
        }
    }
}
