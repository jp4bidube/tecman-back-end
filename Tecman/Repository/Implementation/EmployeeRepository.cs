using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Models.Context;
using Tecman.Services;

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

        public Employee FindById(int id)
        {
            return _context.Employee.FirstOrDefault(element => element.id.Equals(id));
        }
    }
}
