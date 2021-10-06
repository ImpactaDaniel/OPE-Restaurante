using Microsoft.IdentityModel.Tokens;
using Restaurante.Domain.Users.Common.Models;
using Restaurante.Domain.Users.Common.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                    new Claim("name", user.Name),
                    new Claim("firstAccess", user.FirstAccess.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(TokenConfiguration.ValidTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new TokenResponse(tokenHandler.WriteToken(token), token.ValidFrom, token.ValidTo, user.FirstAccess);
        }

        public int? GetIdByToken(string token, bool validateLifeTime)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(TokenConfiguration.Secret);
            var tokenValidation = new TokenValidationParameters
            {
                ValidateLifetime = validateLifeTime,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, tokenValidation, out _);
                return int.Parse(principal.Claims.Where(c => c.Type == ClaimTypes.Sid).FirstOrDefault()?.Value);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
