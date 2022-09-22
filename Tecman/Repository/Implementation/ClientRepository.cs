using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Models.Context;
using Tecman.Services;
using Tecman.ValueObject;

namespace Tecman.Repository.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private PostgreSqlDbContext _context;
        private IResponseApiService _response;
        public ClientRepository(PostgreSqlDbContext context, IResponseApiService response)
        {
            _response = response;
            _context = context;
        }
        public int CountListClient(string q)
        {
            return _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q)).Count();
        }

        public Client Create(Client client)
        {
            try
            {
                _context.Add(client);
                _context.SaveChanges();

                return client;
            }
            catch (Exception e)
            {
                return null;

            }
        }

        public ClientAddress CreateClientAddress(ClientAddress clientAddress)
        {
            try
            {
                _context.Add(clientAddress);
                _context.SaveChanges();

                return clientAddress;
            }
            catch (Exception e)
            {
                return null;

            }
        }

        public Client FindByCPF(string cpf)
        {
            return _context.Client.FirstOrDefault(element => element.cpf.Equals(cpf));
        }

        public Client FindById(int id)
        {
            return _context.Client.FirstOrDefault(element => element.id.Equals(id));
        }

        public List<Client> GetListClientOrderByCPF(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderByDescending(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
            else
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderBy(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
        }

        public List<Client> GetListClientOrderByEmail(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderByDescending(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
            else
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderBy(prop => prop.email)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
        }

        public List<Client> GetListClientOrderByName(String sortDirection, int limit, int offset, String q)
        {
            if (sortDirection == "desc")
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderByDescending(prop => prop.name)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
            else
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderBy(prop => prop.name)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
        }

        public List<Client> GetListClientOrderByNumber(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderByDescending(prop => prop.phoneNumber)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
            else
            {
                List<Client> client = _context.Client.Where(prop => prop.cpf.ToUpper().Contains(q) || prop.email.ToUpper().Contains(q) || prop.name.ToUpper().Contains(q) || prop.phoneNumber.ToUpper().Contains(q))
                    .OrderBy(prop => prop.phoneNumber)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return client;
            }
        }

        public ApiMessage Update(Client client)
        {
            var result = _context.Client.SingleOrDefault(p => p.id.Equals(client.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(client);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            return new ApiMessage
            {
                Success = true,
                Result = result
            };
        }
    }
}
