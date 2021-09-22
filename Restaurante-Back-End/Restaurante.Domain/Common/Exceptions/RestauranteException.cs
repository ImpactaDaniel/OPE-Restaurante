using Restaurante.Domain.Common.Enums;
using System;

namespace Restaurante.Domain.Common.Exceptions
{
    public abstract class RestauranteException : Exception
    {
        public NotificationKeys Code { get; }
        protected RestauranteException(string message, NotificationKeys code) : base(message)
        {
            Code = code;
        }
    }
}
