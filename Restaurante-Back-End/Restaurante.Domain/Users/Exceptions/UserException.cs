using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;

namespace Restaurante.Domain.Users.Exceptions
{
    public class UserException : RestauranteException
    {
        public UserException(string message, NotificationKeys code) : base(message, code)
        {
        }
    }
}
