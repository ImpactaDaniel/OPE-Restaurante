using Restaurante.Domain.Common.Models;

namespace Restaurante.Domain.Users.Funcionarios.Models
{
    public class Bank : Entity<int>
    {
        public string Name { get; private set; }
        private Bank()
        {
        }
        public Bank(string name)
        {
            Name = name;
        }
    }
}