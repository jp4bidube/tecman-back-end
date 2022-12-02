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
    public class AddressRepository : IAddressRepository
    {
        private PostgreSqlDbContext _context;
        private IResponseApiService _response;
        public AddressRepository(PostgreSqlDbContext context, IResponseApiService response)
        {
            _response = response;
            _context = context;
        }

        public Address Create(Address address)
        {
            try
            {
                _context.Add(address);
                _context.SaveChanges();

                return address;
            }
            catch (Exception e)
            {
                return null;

            };
        }

        public Address findById(int id)
        {
            return _context.Address.FirstOrDefault(element => element.id.Equals(id));
        }

        public ApiMessage Update(Address address)
        {
            var result = _context.Address.SingleOrDefault(p => p.id.Equals(address.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(address);
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
