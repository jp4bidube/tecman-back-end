using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;
using Tecman.ValueObject.ClientObjects;

namespace Tecman.Services
{
    public interface IAddressService
    {
        Address Create(Address address);

        ApiMessage Update(Address address);
        bool UpdateClientAddress(Address address, ClientAddressUpdate clientAddressUpdate);

        Address findById(int id);

    }
}
