using Restaurante.Application.Common;
using Restaurante.Domain.Users.Enums;

namespace Restaurante.Application.Users.Common.Models
{
    public abstract class EmployeeRequest<TRequest> : EntityRequest<int>
        where TRequest : EntityRequest<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EmployeesType Type { get; set; }
    }
}
