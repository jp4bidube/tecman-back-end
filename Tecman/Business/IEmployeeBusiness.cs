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
        Employee FindByCPF(string cpf);

        EmployeeStatus FindEmployeeStatusById(int id);

        Role FindRoleById(int id);

        bool DisableEnableEmployee(Employee employee);
        bool UpdateAddressEmployee(Employee employee, AddressObject addressObject);

        bool Create(EmployeeCreate employeeCreate);
        bool Update(Employee employee,EmployeeUpdate employeeUpdate);

        List<Employee> GetListEmployee(String sortDirection, int limit, int offset, String search, String sort);
        int CountListEmployee(String search);
    }
}
