using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.Business
{
    public interface IEmployeeBusiness
    {
        Employee FindById(int id);
    }
}
