using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.Business.Implementation
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private IResponseApiService _response;
        private IEmployeeRepository _repository;
        private IUserRepository _user;
        private IAddressService _address;


        public EmployeeBusiness(IResponseApiService response, IEmployeeRepository repository, IUserRepository user, IAddressService address)
        {
            _response = response;
            _repository = repository;
            _user = user;
            _address = address;

        }

        public int CountListEmployee(string search)
        {
            return _repository.CountListEmployee(search);
        }

        public int CountListTecnic(string search)
        {
            return _repository.CountListTecnic(search);
        }


        public bool Create(EmployeeCreate employeeCreate)
        {
            Address address = null;
            ApiMessage user;

            if (employeeCreate.address.cep != null)
            {
                Address addressCreate = new Address
                {
                    cep = employeeCreate.address.cep,
                    street = employeeCreate.address.street,
                    district = employeeCreate.address.district,
                    number = employeeCreate.address.number,
                    complement = employeeCreate.address.complement,
                };

                address = _address.Create(addressCreate);
            }

            Employee employeeNew = new Employee
            {
                address = address,
                employeeStatus = _repository.FindEmployeeStatusById(1),
                avatarUrl = employeeCreate.avatarUrl,
                birthDate = employeeCreate.birthDate,
                cpf = employeeCreate.cpf,
                email = employeeCreate.email,
                name = employeeCreate.name,
                phoneNumber = employeeCreate.phoneNumber,
                registrationDate = DateTime.Now,
                role = _repository.FindRoleById(employeeCreate.role)
                
            };

            Employee employee = _repository.Create(employeeNew);

            if (employee == null) return false;

            if(employeeCreate.employeeUser.login == true)
            {
                User userCreate = new User
                {
                    username = employeeCreate.employeeUser.username,
                    password = employeeCreate.employeeUser.password,
                    employee = employee,
                };

                user = _user.Create(userCreate);

                if (user.Success == false) return false;
            }

            return true;
           
        }

        public bool DisableEnableEmployee(Employee employee)
        {
            if(employee.employeeStatus.id == 1)
            {
                employee.employeeStatus = _repository.FindEmployeeStatusById(2);
            } else
            {
                employee.employeeStatus = _repository.FindEmployeeStatusById(1);
            }

            ApiMessage update = _repository.Update(employee);

            return update.Success;
        }

        public Employee FindByCPF(string cpf)
        {
            return _repository.FindByCPF(cpf);
        }

        public EmployeeUnique FindById(int id)
        {
            Employee employee = _repository.FindById(id);
            EmployeeUnique employeeUnique = new EmployeeUnique
            {
                id = employee.id,
                address = employee.address,
                employeeStatus = employee.employeeStatus,
                avatarUrl = employee.avatarUrl,
                birthDate = employee.birthDate,
                cpf = employee.cpf,
                email = employee.email,
                name = employee.name,
                phoneNumber = employee.phoneNumber,
                role = employee.role,
                user = _user.FindByEmployeeId(employee.id)
            };
            
            if(employee.role.role == "tecnico")
            {
                List<TenicInfoGrouped> status = _repository.getInfoOsByTecnic(employee.id);
                employeeUnique.orderServicesCanceled = 0;
                employeeUnique.orderServicesBudget = 0;
                employeeUnique.orderServicesDone = 0;

                foreach (TenicInfoGrouped tenicInfoGrouped in status)
                {
                    if(tenicInfoGrouped.name == "Em orçamento")
                    {
                        employeeUnique.orderServicesBudget = tenicInfoGrouped.count;
                    } else if (tenicInfoGrouped.name == "Realizada")
                    {
                        employeeUnique.orderServicesDone = tenicInfoGrouped.count;
                    }else if(tenicInfoGrouped.name == "Cancelada")
                    {
                        employeeUnique.orderServicesCanceled = tenicInfoGrouped.count;
                    }
                }

                employeeUnique.orderServicesTotal = employeeUnique.orderServicesCanceled + employeeUnique.orderServicesBudget + employeeUnique.orderServicesDone;

            }

            return employeeUnique;
        }

        public Employee Find(int id)
        {
            return _repository.FindById(id);
        }

        public EmployeeStatus FindEmployeeStatusById(int id)
        {
            return _repository.FindEmployeeStatusById(id);
        }

        public Role FindRoleById(int id)
        {
            return _repository.FindRoleById(id);
        }

        public List<Employee> GetListTecnic(String sortDirection, int limit, int offset, String search, String sort)
        {
            switch (sort)
            {
                case "name":
                    return _repository.GetListTecnicOrderByName(sortDirection, limit, offset, search);
                    break;
                case "cpf":
                    return _repository.GetListTecnicOrderByCPF(sortDirection, limit, offset, search);
                    break;
                case "status":
                    return _repository.GetListTecnicOrderByStatus(sortDirection, limit, offset, search);
                    break;
                case "role":
                    return _repository.GetListTecnicOrderByRole(sortDirection, limit, offset, search);
                    break;
                case "email":
                    return _repository.GetListTecnicOrderByEmail(sortDirection, limit, offset, search);
                    break;
                default:
                    return _repository.GetListTecnicOrderByName(sortDirection, limit, offset, search);
                    break;

            }
        }


        public List<Employee> GetListEmployee(String sortDirection, int limit, int offset, String search, String sort)
        {
            switch (sort)
            {
                case "name":
                    return _repository.GetListEmployeeOrderByName(sortDirection, limit, offset, search);
                    break;
                case "cpf":
                    return _repository.GetListEmployeeOrderByCPF(sortDirection, limit, offset, search);
                    break;
                case "status":
                    return _repository.GetListEmployeeOrderByStatus(sortDirection, limit, offset, search);
                    break;
                case "role":
                    return _repository.GetListEmployeeOrderByRole(sortDirection, limit, offset, search);
                    break;
                case "email":
                    return _repository.GetListEmployeeOrderByEmail(sortDirection, limit, offset, search);
                    break;
                default:
                    return _repository.GetListEmployeeOrderByName(sortDirection, limit, offset, search);
                    break;

            }
        }

        public bool Update(Employee employee, EmployeeUpdate employeeUpdate)
        {
            employee.name = employeeUpdate.name;
            employee.cpf = employeeUpdate.cpf;
            employee.role = _repository.FindRoleById(employeeUpdate.role);
            employee.phoneNumber = employeeUpdate.phoneNumber;
            employee.avatarUrl = employeeUpdate.avatarUrl;
            employee.email = employeeUpdate.email;
            employee.birthDate = employeeUpdate.birthDate;

            if(employee.address != null)
            {
                employee.address.cep = employeeUpdate.address.cep;
                employee.address.district = employeeUpdate.address.district;
                employee.address.complement = employeeUpdate.address.complement;
                employee.address.number = employeeUpdate.address.number;
                employee.address.street = employeeUpdate.address.street;
            }

            ApiMessage updateEmployee = _repository.Update(employee);

            if (updateEmployee.Success == false) return false;

            if(employeeUpdate.employeeUser.login == true)
            {
                User user = _user.FindByEmployeeId(employee.id);

                user.username = employeeUpdate.employeeUser.username;

                ApiMessage updateUser = _user.Update(user);

                if (updateUser.Success == false) return false;
            }

            return true;
        }

        public bool UpdateAddressEmployee(Employee employee, AddressObject addressObject)
        {
            Address adress = _address.findById(employee.address.id);

            adress.street = addressObject.street;
            adress.number = addressObject.number;
            adress.district = addressObject.district;
            adress.cep = addressObject.cep;
            adress.complement = addressObject.complement;

            ApiMessage update = _address.Update(adress);

            return update.Success;

        }

        public List<TecnicListSelect> ListTecnicSelect()
        {
           return _repository.ListTecnicSelect();
        }

        public List<TenicInfoGrouped> getInfoOsByTecnic(int id)
        {
            return _repository.getInfoOsByTecnic(id);
        }

    }
}
