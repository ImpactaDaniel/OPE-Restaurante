using Restaurante.Application.Common;
using System;
using System.Collections.Generic;

namespace Restaurante.Application.Users.Common.Models
{
    public abstract class CustomerRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressRequest Address { get; set; }
        public IEnumerable<PhoneRequest> Phones { get; set; }        
    }
}
