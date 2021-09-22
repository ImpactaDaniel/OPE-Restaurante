using FizzWare.NBuilder;
using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Entregadores.Models;
using System.Collections;
using System.Collections.Generic;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class EntregadorMock
    {
        public static Vehicle GetDefaultVehicle() =>
            Builder<Vehicle>
            .CreateNew()
            .WithFactory(() => new Vehicle("Kawasaki", "Suzuki", 2010))
            .Build();

        public static DeliveryPerson GetDefaulEntregador() =>
            Builder<DeliveryPerson>
            .CreateNew()
            .WithFactory(() => new DeliveryPerson("Daniel", "daniel@gmail.com", "123456", GetDefaultVehicle(), AccountMock.GetDefault(), new List<Phone>() { PhoneMock.GetDefault() }, AddressMock.GetDefault()))
            .Build();
    }

    public class EntregadoresInvalidos : IEnumerable<object[]>
    {
        private readonly IList<object[]> _data = new List<object[]>(4);
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
