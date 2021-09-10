using Restaurante.Application.Common;
using Restaurante.Domain.Users.Enums;
using System;
using System.Collections.Generic;

namespace Restaurante.Application.Users.Common.Models
{
    public abstract class EmployeeRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int BankId { get; set; }
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public int Digit { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
        public Dictionary<string, string> Phones { get; set; }
        public EmployeesType Type { get; set; }
    }
}
