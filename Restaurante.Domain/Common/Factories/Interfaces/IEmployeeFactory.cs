using Restaurante.Domain.Users.Employees.Models;
using Restaurante.Domain.Users.Enums;

namespace Restaurante.Domain.Common.Factories.Interfaces
{
    public interface IEmployeeFactory: IFactory<Employee>, IUserFactory<Employee>
    {
        IEmployeeFactory WithType(EmployeesType type);
        IEmployeeFactory WithPhone(Phone phone);
        IEmployeeFactory WithAddress(Address address);
        IEmployeeFactory WithAccount(Account account);
    }
}
