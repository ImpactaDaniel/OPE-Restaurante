using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Employees.Models
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
            ValidateNullString(street, "Rua");
            ValidateNullString(number, "Número");
            ValidateNullString(district, "Bairro");
            ValidateNullString(cep, "CEP");
            Street = street;
            Number = number;
            District = district;
            CEP = cep;
        }

        public Address UpdateStreet(string street)
        {
            ValidateNullString(street, "Rua");
            if (Street != street)
                Street = street;
            return this;
        }

        public Address UpdateNumber(string number)
        {
            ValidateNullString(Number, "Número");
            if (Number != number)
                Number = number;
            return this;
        }
        public Address UpdateDistrict(string district)
        {
            ValidateNullString(District, "Bairro");
            if (District != district)
                District = district;
            return this;
        }

        public Address UpdateCEP(string cep)
        {
            ValidateNullString(CEP, "CEP");
            if (CEP != cep)
                CEP = cep;
            return this;
        }
    }
}