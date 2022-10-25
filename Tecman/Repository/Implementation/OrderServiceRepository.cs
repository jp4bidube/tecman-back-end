using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Models.Context;
using Tecman.Services;

namespace Tecman.Repository.Implementation
{
    public class OrderServiceRepository : IOrderServiceRepository
    {
        private PostgreSqlDbContext _context;
        private IResponseApiService _response;
        public OrderServiceRepository(PostgreSqlDbContext context, IResponseApiService response)
        {
            _response = response;
            _context = context;
        }
        public OrderService Create(OrderService order)
        {
            try
            {
                _context.Add(order);
                _context.SaveChanges();

                return order;
            }
            catch (Exception e)
            {
                return null;

            }
        }

        public bool CreateEquipment(Equipment equipment)
        {
            try
            {
                _context.Add(equipment);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }

        public OrderService FindById(int id)
        {
            return _context.OrderService.Find(id);
        }

        public OrderServiceStatus OrderServiceStatusFindById(int id)
        {
            return _context.OrderServiceStatus.Find(id);
        }
    }
}
