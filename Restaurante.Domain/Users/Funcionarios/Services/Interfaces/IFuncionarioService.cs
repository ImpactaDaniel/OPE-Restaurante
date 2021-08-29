using Restaurante.Domain.Common.Services.Interfaces;
using Restaurante.Domain.Users.Funcionarios.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Funcionarios.Services.Interfaces
{
    public interface IFuncionarioService<TFuncionario> : IEntityService<TFuncionario>
        where TFuncionario : Funcionario
    {
        Task<bool> CreateFuncionario(TFuncionario funcionario, Funcionario usuario, CancellationToken cancellationToken = default);
        Task<bool> Delete(int id, Funcionario usuario, CancellationToken cancellationToken = default);
        Task<TFuncionario> Login(string email, string password, CancellationToken cancellationToken = default);
        Task<IList<TFuncionario>> GetAll(CancellationToken cancellationToken = default);
    }
}
