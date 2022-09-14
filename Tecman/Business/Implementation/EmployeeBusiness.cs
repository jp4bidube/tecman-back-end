using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;

namespace Tecman.Business.Implementation
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private IResponseApiService _response;
        private IEmployeeRepository _repository;


        public EmployeeBusiness(IResponseApiService response, IEmployeeRepository repository)
        {
            _response = response;
            _repository = repository;

        }
        public Employee FindById(int id)
        {
            return _repository.FindById(id);
        }
    }
}
