using System;

namespace Restaurante.Domain.Common.Models.Integration
{
    public class EntregadorIntegration
    {
        public UserData User { get; set; }
        public PersonalData Personal { get; set; }
        public PaymentData Payment { get; set; }
        public Motocycle Motorcycle { get; set; }
    }

    public class UserData
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class PersonalData
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string Rg { get; set; }
        public PhoneData Phone { get; set; }
        public AddressData Address { get; set; }
    }

    public class PhoneData
    {
        public string Area { get; set; }
        public string Number { get; set; }
    }

    public class AddressData
    {
        public string City { get; set; }
        public string State { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
    }

    public class PaymentData
    {
        public string BankCode { get; set; }
        public string Number { get; set; }
        public string AgencyCode { get; set; }
    }

    public class Motocycle
    {
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
    }
}
