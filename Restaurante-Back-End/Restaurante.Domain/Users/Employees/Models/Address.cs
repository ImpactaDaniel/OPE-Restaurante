using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Exceptions;
using System.Linq;

namespace Restaurante.Domain.Users.Employees.Models
{
    public class Address : Entity<int>
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string District { get; private set; }
        public string CEP { get; private set; }
        public string State { get; private set; }
        public string City { get; private set; }
        private Address()
        {
        }

        public Address(string street, string number, string district, string cep, string state, string city)
            : this()
        {
            ValidateNullString(street, "Rua");
            ValidateNullString(number, "Número");
            ValidateNullString(district, "Bairro");
            ValidateNullString(cep, "CEP");
            ValidateNullString(state, "Estado");
            ValidateNullString(city, "Cidade");
            ValidateCEP(cep);
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
            ValidateNullString(number, "Número");
            if (Number != number)
                Number = number;
            return this;
        }
        public Address UpdateDistrict(string district)
        {
            ValidateNullString(district, "Bairro");
            if (District != district)
                District = district;
            return this;
        }

        public Address UpdateCEP(string cep)
        {
            ValidateNullString(cep, "CEP");
            ValidateCEP(cep);
            if (CEP != cep)
                CEP = cep;
            return this;
        }

        public Address UpdateCity(string city)
        {
            ValidateNullString(city, "Cidade");
            if (City != city)
                City = city;
            return this;
        }

        public Address UpdateState(string state)
        {
            ValidateNullString(state, "Estado");
            if (State != state)
                State = state;
            return this;
        }

        private static void ValidateCEP(string cep)
        {
            if (!cep.All(char.IsDigit) || cep.Length != 8)
                throw new UserException("CEP deve conter somente dígitos com 8 caracteres!", NotificationKeys.InvalidEntity);
        }
    }
}