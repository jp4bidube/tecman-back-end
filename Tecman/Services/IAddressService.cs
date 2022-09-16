using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;

namespace Tecman.Services
{
    public interface IAddressService
    {
        Address Create(Address address);

        ApiMessage Update(Address address);

        Address findById(int id);

    }
}
