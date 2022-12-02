using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject.ClientObjects;

namespace Tecman.Business
{
   public interface IClientBusiness
    {
        ClientUnique FindById(int id);
        Client Find(int id);
        ClientUnique FindByCPF(string cpf);

        bool Create(ClientCreate clientCreate);
        ClientAddress CreateClientAddress(int addressId, int clientId);
        bool Update(Client client, ClientUpdate clientUpdate);
        ClientAddress GetClientAddress(int clientId, int addressId);
        bool SetDefault(ClientAddress clientAddress);

        List<Client> GetListClient(String sortDirection, int limit, int offset, String search, String sort);
        int CountListClient(String search);
    }
}
