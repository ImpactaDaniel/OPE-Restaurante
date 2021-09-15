using Restaurante.Domain.Common.Models;
using Restaurante.Domain.Users.Exceptions;
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
            ValidateNullString(branch, "Agência");
            ValidateNullString(accountNumber, "Número da conta");
            Bank = bank ?? throw new UserException(nameof(bank));
            Branch = branch;
            AccountNumber = accountNumber;
            Digit = digit;
        }

        public Account UpdateBank(Bank bank)
        {
            bank = bank ?? throw new UserException(nameof(bank));
            if (Bank.Id != bank?.Id)
                Bank = bank;
            return this;
        }

        public Account UpdateBranch(string branch)
        {
            ValidateNullString(branch, "Agência");
            if (Branch != branch)
                Branch = branch;
            return this;
        }

        public Account UpdateAccountNumber(string accountNumber)
        {
            ValidateNullString(accountNumber, "Número da conta");
            if (AccountNumber != accountNumber)
                AccountNumber = accountNumber;
            return this;
        }

        public Account UpdateDigit(int digit)
        {
            if (Digit != digit)
                Digit = digit;
            return this;
        }
    }
}