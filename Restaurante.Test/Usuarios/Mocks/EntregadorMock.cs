using FizzWare.NBuilder;
using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Domain.Users.Funcionarios.Models;
using System.Collections;
using System.Collections.Generic;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class EntregadorMock
    {
        public static Veiculo GetDefaultVehicle() =>
            Builder<Veiculo>
            .CreateNew()
            .WithFactory(() => new Veiculo("Kawasaki", "Suzuki", 2010))
            .Build();

        public static Entregador GetDefaulEntregador() =>
            Builder<Entregador>
            .CreateNew()
            .WithFactory(() => new Entregador("Daniel", "daniel@gmail.com", "123456", GetDefaultVehicle(), new Account(new Bank("'"), "", "", 0), new List<Phone>(), new Address("", "", "", "")))
            .Build();
    }

    public class EntregadoresInvalidos : IEnumerable<object[]>
    {
        private IList<object[]> _data = new List<object[]>(4);
        public EntregadoresInvalidos()
        {
            _data.Add(new object[]
            {
                string.Empty, "danielcity1@gmail.com", "123456", EntregadorMock.GetDefaultVehicle(), "Nome precisa estar preenchido!"
            });
            _data.Add(new object[]
            {
                "Daniel", string.Empty, "123456", EntregadorMock.GetDefaultVehicle(), "E-mail precisa estar preenchido!"
            });
            _data.Add(new object[]
            {
                "Daniel", "daniel@gmail.com", string.Empty, EntregadorMock.GetDefaultVehicle(), "Senha precisa estar preenchido!"
            });
            _data.Add(new object[]
            {
                "Daniel", "daniel@gmail.com", "123456", null, "Veículo inválido!"
            });
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
