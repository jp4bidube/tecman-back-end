using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Models.Context;
using Tecman.Services;
using Tecman.ValueObject;
using System.Linq.Dynamic;
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.Repository.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private PostgreSqlDbContext _context;
        private IResponseApiService _response;
        public EmployeeRepository(PostgreSqlDbContext context, IResponseApiService response)
        {
            _response = response;
            _context = context;
        }

        public int CountListEmployee(string q)
        {
            return _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4).Count();
        }
        public int CountListTecnic(string q)
        {
            return _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4).Count();
        }


        public Employee Create(Employee employee)
        {
            try
            {
                _context.Add(employee);
                _context.SaveChanges();

                return employee;
            }
            catch (Exception e)
            {
                return null;

            }
        }

        public Employee FindById(int id)
        {
            return _context.Employee.FirstOrDefault(element => element.id.Equals(id));
        }
        public Employee FindByCPF(string cpf)
        {
            return _context.Employee.FirstOrDefault(element => element.cpf.Equals(cpf));
        }


        public EmployeeStatus FindEmployeeStatusById(int id)
        {
            return _context.EmployeeStatus.FirstOrDefault(element => element.id.Equals(id));
        }

        public Role FindRoleById(int id)
        {
            return _context.Role.FirstOrDefault(element => element.id.Equals(id));
        }

        public List<Employee> GetListEmployeeOrderByCPF(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderByDescending(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderBy(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }

        public List<Employee> GetListTecnicOrderByCPF(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderByDescending(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderBy(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }


        public List<Employee> GetListEmployeeOrderByEmail(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderByDescending(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderBy(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }

        public List<Employee> GetListTecnicOrderByEmail(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderByDescending(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderBy(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }


        public List<Employee> GetListEmployeeOrderByName(String sortDirection , int limit, int offset, String q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderByDescending(prop => prop.name)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderBy(prop => prop.name)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }

        public List<Employee> GetListTecnicOrderByName(String sortDirection, int limit, int offset, String q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderByDescending(prop => prop.name)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderBy(prop => prop.name)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }


        public List<Employee> GetListEmployeeOrderByRole(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderByDescending(prop => prop.role.role)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderBy(prop => prop.role.role)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }

        public List<Employee> GetListTecnicOrderByRole(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderByDescending(prop => prop.role.role)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderBy(prop => prop.role.role)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }


        public List<Employee> GetListEmployeeOrderByStatus(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderByDescending(prop => prop.employeeStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id != 4)
                    .OrderBy(prop => prop.employeeStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }

        public List<Employee> GetListTecnicOrderByStatus(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderByDescending(prop => prop.employeeStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
            else
            {
                List<Employee> employee = _context.Employee.Where(prop => (prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.role.role.ToUpper().Contains(q) || prop.employeeStatus.status.ToUpper().Contains(q)) && prop.role.id == 4)
                    .OrderBy(prop => prop.employeeStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return employee;
            }
        }

        public ApiMessage Update(Employee employee)
        {
            var result = _context.Employee.SingleOrDefault(p => p.id.Equals(employee.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(employee);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return new ApiMessage
            {
                Success = true,
                Result = result
            };
        }

        public List<TecnicListSelect> ListTecnicSelect()
        {
            return _context.Employee.Where(prop => prop.role.id == 4 && prop.employeeStatus.id == 1).Select(prop => new TecnicListSelect { id= prop.id, name = prop.name }).ToList();
        }
    }
}
