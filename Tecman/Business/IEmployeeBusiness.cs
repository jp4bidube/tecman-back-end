using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;

namespace Tecman.Business
{
    public interface IEmployeeBusiness
    {
        Employee FindById(int id);

        EmployeeStatus FindEmployeeStatusById(int id);

        Role FindRoleById(int id);

        bool DisableEnableEmployee(Employee employee);
        bool UpdateAddressEmployee(Employee employee, AddressObject addressObject);

        bool Create(EmployeeCreate employeeCreate);

        List<Employee> GetListEmployee(String sortDirection, int limit, int offset, String search, String sort);
    }
}
