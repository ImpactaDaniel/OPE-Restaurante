using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Funcionarios.Models
{
    public class Address : Entity<int>
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string CEP { get; private set; }
        private Address()
        {
        }
        public Address(string street, string number, string district, string cep)
        {
            Street = street;
            Number = number;
            District = district;
            CEP = cep;
        }
    }
}