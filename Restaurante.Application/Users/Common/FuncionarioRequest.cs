using Restaurante.Application.Common;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios;

namespace Restaurante.Application.Users.Common
{
    public abstract class FuncionarioRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public FuncionarioRequest(Funcionario funcionario)
        {
            Name = funcionario.Name;
            Email = funcionario.Email;
            Password = funcionario.Password;
            Type = funcionario.Type;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public TiposFuncionario Type { get; set; }
    }
}
