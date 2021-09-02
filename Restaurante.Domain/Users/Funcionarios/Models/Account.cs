using Restaurante.Domain.Common.Models;
using System;

namespace Restaurante.Domain.Users.Funcionarios.Models
{
    public class Account : Entity<int>
    {
        public Bank Bank { get; private set; }
        public string Branch { get; private set; }
        public string AccountNumber { get; private set; }
        public int Digit { get; private set; }
        private Account()
        {

        }
        public Account(Bank bank, string branch, string accountNumber, int digit)
        {
            Bank = bank;
            Branch = branch;
            AccountNumber = accountNumber;
            Digit = digit;
        }
    }
}