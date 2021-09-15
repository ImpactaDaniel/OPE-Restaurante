using Restaurante.Domain.Users.Common.Models;

namespace Restaurante.Web.Models
{
    public class LoginResponse
    {
        public TokenResponse TokenResponse { get; private set; }
        public User User { get; private set; }
        public LoginResponse(TokenResponse tokenResponse, User user)
        {
            TokenResponse = tokenResponse;
            User = user;
        }
    }
}
