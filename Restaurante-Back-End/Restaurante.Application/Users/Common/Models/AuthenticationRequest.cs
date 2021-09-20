using MediatR;
using Restaurante.Application.Common;

namespace Restaurante.Application.Users.Common.Models
{
    public class AuthenticationRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
