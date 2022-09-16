﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Models.Context;
using Tecman.Services;
using Tecman.ValueObject;

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

        public EmployeeStatus FindEmployeeStatusById(int id)
        {
            return _context.EmployeeStatus.FirstOrDefault(element => element.id.Equals(id));
        }

        public Role FindRoleById(int id)
        {
            return _context.Role.FirstOrDefault(element => element.id.Equals(id));
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
    }
}
