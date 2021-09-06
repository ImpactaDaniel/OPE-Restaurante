using FizzWare.NBuilder;
using Restaurante.Domain.Users.Enums;
using Restaurante.Domain.Users.Funcionarios.Models;
using System.Collections.Generic;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class FuncionarioMock
    {
        public static Funcionario GetDefaultGerente() =>
            Builder<Funcionario>
            .CreateNew()
            .WithFactory(() => new Funcionario("Carlos", "carlos@gmail.com", "123456", TiposFuncionario.Gerente, new Account(new Bank("'"), "", "", 0), new List<Phone>(), new Address("", "", "", "")))
            .Build();

        public static Funcionario GetDefault() =>
            Builder<Funcionario>
            .CreateNew()
            .WithFactory(() => new Funcionario("Carlos", "carlos@gmail.com", "123456", TiposFuncionario.Funcionario, new Account(new Bank("'"), "", "", 0), new List<Phone>(), new Address("", "", "", "")))
            .Build();
    }
}
