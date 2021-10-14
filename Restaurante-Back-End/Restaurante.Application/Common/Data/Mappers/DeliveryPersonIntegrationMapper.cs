using Restaurante.Domain.Common.Models.Integration;
using Restaurante.Domain.Users.Entregadores.Models;
using System.Linq;

namespace Restaurante.Application.Common.Data.Mappers
{
    public class DeliveryPersonIntegrationMapper : Mapper<DeliveryPerson, DeliveryPersonIntegration>
    {
        public override DeliveryPersonIntegration Map(DeliveryPerson source) =>
            new()
            {
                Motorcycle = new Motocycle
                {
                    Brand = source.MotoCycle.Brand,
                    Model = source.MotoCycle.Model,
                    Year = source.MotoCycle.Year
                },
                Personal = new PersonalData
                {
                    Name = source.Name,
                    Email = source.Email,
                    Address = new AddressData
                    {
                        City = source.Address.City,
                        District = source.Address.District,
                        Number = source.Address.Number,
                        State = source.Address.State,
                        Street = source.Address.Street,
                        ZipCode = source.Address.CEP
                    },
                    BirthDate = source.BirthDate,
                    CPF = source.Document,
                    Phone = new PhoneData
                    {
                        Area = source.Phones.First().DDD,
                        Number = source.Phones.First().PhoneNumber
                    }                    
                },
                Payment = new PaymentData
                {
                    AgencyCode = source.Account.Branch,
                    BankCode = source.Account.Bank.Id.ToString(),
                    Number = source.Account.AccountNumber
                },
                User = new UserData
                {
                    Password = source.Password
                }
            };
    }
}
