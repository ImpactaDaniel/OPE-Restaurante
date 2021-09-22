using Restaurante.Domain.BasicEntities.Common.Interfaces;
using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Employees.Models
{
    public class Bank : Entity<int>, IBasicEntity
    {
        public string Name { get; private set; }
        private Bank()
        {
        }
        public Bank(string name)
        {
            ValidateNullString(name, "Nome do banco");
            Name = name;
        }

        public Bank UpdateName(string name)
        {
            ValidateNullString(name, "Nome do banco");
            if (Name != name)
                Name = name;
            return this;
        }
    }
}