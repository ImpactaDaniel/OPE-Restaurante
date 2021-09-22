using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.Common.Enums;
using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Exceptions;
using System.Linq;

namespace Restaurante.Domain.Users.Employees.Models
{
    public class Phone : Entity<int>, IBasicEntity
    {
        public string DDD { get; private set; }
        public string PhoneNumber { get; private set; }
        private Phone()
        {

        }
        public Phone(string ddd, string phoneNumber)
        {
            ValidateNullString(ddd, "DDD");
            ValidateNullString(phoneNumber, "Número de telefone");
            ValidatePhoneNumber(phoneNumber);
            ValidateDDD(ddd);
            DDD = ddd;
            PhoneNumber = phoneNumber;
        }

        public Phone UpdateDDD(string ddd)
        {
            ValidateNullString(ddd, "DDD");
            ValidateDDD(ddd);
            if (DDD != ddd)
                DDD = ddd;
            return this;
        }

        public Phone UpdatePhoneNumber(string phoneNumber)
        {
            ValidateNullString(phoneNumber, "Número de telefone");
            ValidatePhoneNumber(phoneNumber);
            if (PhoneNumber != phoneNumber)
                PhoneNumber = phoneNumber;
            return this;
        }

        private static void ValidatePhoneNumber(string phoneNumber)
        {
            if (!phoneNumber.All(char.IsDigit) || phoneNumber.Length > 9 || phoneNumber.Length < 8)
                throw new UserException("Telefone deve conter somente dígitos com 8 ou 9 caracteres!", NotificationKeys.InvalidEntity);
        }

        private static void ValidateDDD(string ddd)
        {
            if (!ddd.All(char.IsDigit) || ddd.Length != 2)
                throw new UserException("DDD deve conter somente dígitos com 2 caracteres!", NotificationKeys.InvalidEntity);
        }
    }
}