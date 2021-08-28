using Restaurante.Domain.Users.Funcionarios;
using FizzWare.NBuilder;
using Restaurante.Domain.Users.Enums;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class FuncionarioMock
    {
        public static Funcionario GetDefaultGerente() =>
            Builder<Funcionario>
            .CreateNew()
            .WithFactory(() => new Funcionario("Carlos", "carlos@gmail.com", "123456", TiposFuncionario.Gerente))
            .Build();

        public static Funcionario GetDefault() =>
            Builder<Funcionario>
            .CreateNew()
            .WithFactory(() => new Funcionario("Carlos", "carlos@gmail.com", "123456", TiposFuncionario.Funcionario))
            .Build();
    }
}
