using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Employees.Models
{
    public class Phone : Entity<int>
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
            DDD = ddd;
            PhoneNumber = phoneNumber;
        }

        public Phone UpdateDDD(string ddd)
        {
            ValidateNullString(ddd, "DDD");
            if (DDD != ddd)
                DDD = ddd;
            return this;
        }

        public Phone UpdatePhoneNumber(string phoneNumber)
        {
            ValidateNullString(phoneNumber, "Número de telefone");
            if (PhoneNumber != phoneNumber)
                PhoneNumber = phoneNumber;
            return this;
        }
    }
}