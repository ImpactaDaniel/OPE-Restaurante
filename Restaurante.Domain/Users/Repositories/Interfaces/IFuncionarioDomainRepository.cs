﻿using Restaurante.Domain.Common.Repositories.Interfaces;
using Restaurante.Domain.Users.Funcionarios;
using System.Threading;
using System.Threading.Tasks;

namespace Restaurante.Domain.Users.Repositories.Interfaces
{
    public interface IFuncionarioDomainRepository<TFuncionario> : IDomainRepository<TFuncionario>
        where TFuncionario : Funcionario
    {
        Task<TFuncionario> Login(string email, string password, CancellationToken cancellationToken = default);
        Task<TFuncionario> Get(int id, CancellationToken cancellationToken = default);        
        Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        Task<TFuncionario> CreateFuncionario(TFuncionario funcionario, Funcionario usuario, CancellationToken cancellationToken = default);
    }
}
