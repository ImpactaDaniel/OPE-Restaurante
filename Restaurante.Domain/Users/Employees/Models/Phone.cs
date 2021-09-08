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
            DDD = ddd;
            PhoneNumber = phoneNumber;
        }
    }
}