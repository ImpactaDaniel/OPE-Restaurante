using Restaurante.Domain.Common.Models;
using System;

namespace Restaurante.Domain.Users.Employees.Models
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

        public Account(Bank bank,
                       string branch,
                       string accountNumber,
                       int digit)
        {
            Bank = bank ?? throw new ArgumentNullException(nameof(bank));
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            AccountNumber = accountNumber ?? throw new ArgumentNullException(nameof(accountNumber));
            Digit = digit;
        }
    }
}