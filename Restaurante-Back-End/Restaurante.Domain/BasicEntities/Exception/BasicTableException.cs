using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Exceptions;

namespace Restaurante.Domain.BasicEntities.Exception
{
    public class BasicTableException : RestauranteException
    {
        public BasicTableException(string message, NotificationKeys code) : base(message, code)
        {
        }
    }
}
