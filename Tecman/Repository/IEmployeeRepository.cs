using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.Repository
{
    public interface IEmployeeRepository
    {
        Employee FindById(int id);
        Employee FindByCPF(string cpf);

        EmployeeStatus FindEmployeeStatusById(int id);

        Role FindRoleById(int id);

        ApiMessage Update(Employee employee);

        List<TecnicListSelect> ListTecnicSelect();


        Employee Create(Employee employee);
        List<Employee> GetListEmployeeOrderByName(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListTecnicOrderByName(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByEmail(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListTecnicOrderByEmail(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByRole(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListTecnicOrderByRole(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByStatus(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListTecnicOrderByStatus(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListEmployeeOrderByCPF(String sortDirection, int limit, int offset, String q);
        List<Employee> GetListTecnicOrderByCPF(String sortDirection, int limit, int offset, String q);
        int CountListEmployee(String q);
        int CountListTecnic(String q);


    }
}
