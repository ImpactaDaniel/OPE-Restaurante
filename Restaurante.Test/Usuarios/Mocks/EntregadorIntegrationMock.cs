using FizzWare.NBuilder;
using Restaurante.Domain.Common.Models.Integration;

namespace Restaurante.Test.Usuarios.Mocks
{
    public static class EntregadorIntegrationMock
    {
        public static EntregadorIntegration GetDefault() =>
            Builder<EntregadorIntegration>
            .CreateNew()
            .Build();
    }
}
