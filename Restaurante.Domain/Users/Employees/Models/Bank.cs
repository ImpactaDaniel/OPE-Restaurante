using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Employees.Models
{
    public class Bank : Entity<int>
    {
        public string Name { get; private set; }
        private Bank()
        {
        }
        public Bank(string name)
        {
            ValidateNullString(name, "Nome");
            Name = name;
        }

        public Bank UpdateName(string name)
        {
            ValidateNullString(name, "Nome");
            if (Name != name)
                Name = name;
            return this;
        }
    }
}