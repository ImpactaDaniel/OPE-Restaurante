using Restaurante.Domain.Users.Common.Models;

namespace Restaurante.Domain.Users.Common.Services.Interfaces
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(User user);
    }
}
