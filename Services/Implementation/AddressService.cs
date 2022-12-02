using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.ValueObject;
using Tecman.ValueObject.ClientObjects;

namespace Tecman.Services.Implementation
{
    public class AddressService : IAddressService
    {
        public IAddressRepository _repository;

        public AddressService(IAddressRepository repository)
        {
            _repository = repository;
        }

        public Address Create(Address address)
        {
            return _repository.Create(address);
        }

        public Address findById(int id)
        {
            return _repository.findById(id);
        }

        public ApiMessage Update(Address address)
        {
            return _repository.Update(address);
        }

        public bool UpdateClientAddress(Address address, ClientAddressUpdate clientAddressUpdate)
        {
            address.cep = clientAddressUpdate.cep;
            address.street = clientAddressUpdate.street;
            address.district = clientAddressUpdate.district;
            address.number = clientAddressUpdate.number;
            address.complement = clientAddressUpdate.complement;

            ApiMessage update = _repository.Update(address);

            return update.Success;


        }
    }
}
