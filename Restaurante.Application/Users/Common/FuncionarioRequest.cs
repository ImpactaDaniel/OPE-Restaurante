using Restaurante.Application.Common;
using Restaurante.Domain.Users.Enums;

namespace Restaurante.Application.Users.Common
{
    public abstract class FuncionarioRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TiposFuncionario Type { get; set; }
    }
}
