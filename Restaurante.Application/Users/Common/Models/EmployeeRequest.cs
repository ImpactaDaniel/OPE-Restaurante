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
        public AccountRequest Account { get; set; }
        public BankRequest Bank { get; set; }
        public AddressRequest Address { get; set; }
        public List<PhoneRequest> Phones { get; set; }
        public EmployeesType Type { get; set; }
    }
    public class BankRequest
    {
        public int BankId { get; set; }
    }
    public class AccountRequest
    {
        public string Branch { get; set; }
        public string AccountNumber { get; set; }
        public int Digit { get; set; }
    }

    public class AddressRequest
    {
        public string Street { get; set; }
        public string Number { get; set; }
        public string District { get; set; }
        public string CEP { get; set; }
    }

    public class PhoneRequest
    {
        public string DDD { get; set; }
        public string PhoneNumber { get; set; }
    }
}
