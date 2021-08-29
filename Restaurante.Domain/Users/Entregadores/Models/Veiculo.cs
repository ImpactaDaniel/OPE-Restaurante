using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Entregadores.Models
{
    public class Veiculo : Entity<int>
    {
        public string Model { get; private set; }
        public string Brand { get; private set; }
        public int Year { get; private set; }
        public Veiculo(string model, string brand, int year)
        {
            ValidateNullString(model, "Modelo");
            ValidateNullString(brand, "Marca");
            Model = model;
            Brand = brand;
            Year = year;
        }
    }
}
