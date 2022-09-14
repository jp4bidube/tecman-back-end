using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.Repository
{
    public interface IEmployeeRepository
    {
        Employee FindById(int id);

    }
}
