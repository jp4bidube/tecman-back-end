using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.ClientObjects;

namespace Tecman.Business.Implementation
{
    public class ClientBusiness : IClientBusiness
    { 
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private IResponseApiService _response;
        private IClientRepository _repository;
        private IAddressService _address;

        public ClientBusiness(IResponseApiService response, IClientRepository repository, IAddressService address)
    {
        _response = response;
        _repository = repository;
        _address = address;
    }
    
        public int CountListClient(string search)
        {
            return _repository.CountListClient(search);
        }

        public bool Create(ClientCreate clientCreate)
        {
            Client client = new Client
            {
                name = clientCreate.name,
                cpf = clientCreate.cpf,
                email = clientCreate.email,
                phoneNumber = clientCreate.phoneNumber,
            };

            Client createClient = _repository.Create(client);

            if (createClient == null) return false;

            Address address = new Address
            {
                street = clientCreate.address.street,
                cep = clientCreate.address.cep,
                district = clientCreate.address.district,
                complement = clientCreate.address.complement,
                number = clientCreate.address.number,
            };

            Address createAddress = _address.Create(address);

            if (createAddress == null) return false;

            ClientAddress clientAddress = new ClientAddress
            {
                address = createAddress,
                defaultAddress = clientCreate.address.defaultAddress,
                clientId = createClient.id,
            };

            ClientAddress createclientAddress = _repository.CreateClientAddress(clientAddress);

            if (createclientAddress == null) return false;

            return true;
        }

        public Client FindByCPF(string cpf)
        {
            return _repository.FindByCPF(cpf);
        }

        public ClientUnique FindById(int id)
        {
            Client client = _repository.FindById(id);
            List<ClientAddress> clientAdress = _repository.getAllClientAddressByClientId(client.id);
            ClientUnique clientUnique = new ClientUnique
            {
                phoneNumber = client.phoneNumber,
                cpf = client.cpf,
                name = client.name,
                email = client.email,
                address = clientAdress
            };

            return clientUnique;
        }
        public Client Find(int id)
        {
            return _repository.FindById(id);
        }

        public List<Client> GetListClient(string sortDirection, int limit, int offset, string search, string sort)
        {
            switch (sort)
            {
                case "name":
                    return _repository.GetListClientOrderByName(sortDirection, limit, offset, search);
                    break;
                case "cpf":
                    return _repository.GetListClientOrderByCPF(sortDirection, limit, offset, search);
                    break;
                case "phone":
                    return _repository.GetListClientOrderByNumber(sortDirection, limit, offset, search);
                    break;
                case "email":
                    return _repository.GetListClientOrderByEmail(sortDirection, limit, offset, search);
                    break;
                default:
                    return _repository.GetListClientOrderByName(sortDirection, limit, offset, search);
                    break;

            }
        }

        public bool Update(Client client, ClientUpdate clientUpdate)
        {
            client.name = clientUpdate.name;
            client.cpf = clientUpdate.cpf;
            client.phoneNumber = clientUpdate.phoneNumber;
            client.email = clientUpdate.email;

            ApiMessage update = _repository.Update(client);

            return update.Success;
        }

        public ClientAddress GetClientAddress(int clientId, int addressId)
        {
           return _repository.GetClientAddress(clientId,addressId);
        }

        public bool SetDefault(ClientAddress clientAddress)
        {
            ClientAddress oldClientAddress = _repository.FindClientAddressDefault(clientAddress.clientId);

            oldClientAddress.defaultAddress = false;

            bool updateOldClientAddress = _repository.UpdateClientAddress(oldClientAddress);

            if (updateOldClientAddress == false) return false;

            clientAddress.defaultAddress = true;

            bool updateNewClientAddress = _repository.UpdateClientAddress(oldClientAddress);

            if (updateNewClientAddress == false) return false;

            return true;
        }
    }
}
