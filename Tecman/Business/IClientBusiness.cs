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
        Client FindById(int id);
        Client FindByCPF(string cpf);

        bool Create(ClientCreate clientCreate);
        bool Update(Client client, ClientUpdate clientUpdate);

        List<Client> GetListClient(String sortDirection, int limit, int offset, String search, String sort);
        int CountListClient(String search);
    }
}
