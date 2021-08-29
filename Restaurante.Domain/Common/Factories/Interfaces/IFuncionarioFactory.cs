using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Models;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IFuncionarioFactory<TFuncionario> : IFactory<TFuncionario>, IUserFactory<TFuncionario>
        where TFuncionario : Funcionario
    {
        IFuncionarioFactory<TFuncionario> WithType(TiposFuncionario type);
    }
}
