using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Models;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IFuncionarioFactory: IFactory<Funcionario>, IUserFactory<Funcionario>
    {
        IFuncionarioFactory WithType(TiposFuncionario type);
    }
}
