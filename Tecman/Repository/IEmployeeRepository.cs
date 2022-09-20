using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;

namespace Tecman.Repository
{
    public interface IEmployeeRepository
    {
        Employee FindById(int id);

        EmployeeStatus FindEmployeeStatusById(int id);

        Role FindRoleById(int id);

        ApiMessage Update(Employee employee);

        Employee Create(Employee employee);
        List<Employee> GetListEmployeeOrderByName(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByEmail(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByRole(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByStatus(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByCPF(String sortDirection, int limit, int offset, String q);
    }
}
