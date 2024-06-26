﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;

namespace Tecman.Repository
{
    public interface IClientRepository
    {
        Client FindById(int id);
        Client FindByCPF(string cpf);
        ApiMessage Update(Client employee);
        Client Create(Client employee);
        ClientAddress CreateClientAddress(ClientAddress clientAddress);
        List<Client> GetListClientOrderByName(String sortDirection, int limit, int offset, String q);
        List<Client> GetListClientOrderByEmail(String sortDirection, int limit, int offset, String q);
        List<Client> GetListClientOrderByNumber(String sortDirection, int limit, int offset, String q);
        List<Client> GetListClientOrderByCPF(String sortDirection, int limit, int offset, String q);
        List<Client> GetListClientOrderByAddress(String sortDirection, int limit, int offset, String q);
        int CountListClient(String q);
        ClientAddress GetClientAddress(int clientId, int addressId);

        bool UpdateClientAddress(ClientAddress clientAddress);

        ClientAddress FindClientAddressDefault(int clientId);
        List<ClientAddress> getAllClientAddressByClientId(int clientId);

    }
}
