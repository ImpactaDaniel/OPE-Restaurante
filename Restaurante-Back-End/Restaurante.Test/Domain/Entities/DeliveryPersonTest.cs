using Restaurante.Domain.Users.Entregadores.Models;
using Restaurante.Test.Usuarios.Mocks;
using Xunit;

namespace Restaurante.Test.Domain.Entities
{
    public class DeliveryPersonTest
    {
        [Fact]
        public void ShouldUpdateVehicleCorrectly()
        {
            //arrange
            var vehicle = new Vehicle("test", "update", 1990);
            var deliveryMan = EntregadorMock.GetDefaulEntregador();

            //act
            deliveryMan.UpdateVehicle(vehicle);

            //assert
            Assert.Equal(vehicle.Brand, deliveryMan.MotoCycle.Brand);
            Assert.Equal(vehicle.Model, deliveryMan.MotoCycle.Model);
            Assert.Equal(vehicle.Year, deliveryMan.MotoCycle.Year);

        }
    }
}
