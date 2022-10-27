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

        public int CountListOrderService(string q)
        {
            return _context.OrderService.Where(prop => prop.id.ToString().Contains(q)).Count();
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

        public Equipment FindEquipmentById(int? id)
        {
            return _context.Equipment.Find(id);
        }

        public List<Equipment> getAllEquipmentsByOS(int id)
        {
            return _context.Equipment.Where(prop => prop.orderService.id.Equals(id)).ToList();
        }

        public List<OrderService> GetListOrderServiceByEndereco(string sortDirection, int limit, int offset, string q)
        {
            throw new NotImplementedException();
        }

        public List<OrderService> GetListOrderServiceByStatus(string sortDirection, int limit, int offset, string q)
        {
            throw new NotImplementedException();
        }

        public List<OrderService> GetListOrderServiceByTelefone(string sortDirection, int limit, int offset, string q)
        {
            throw new NotImplementedException();
        }

        public List<OrderService> GetListOrderServiceOrderById(string sortDirection, int limit, int offset, string q)
        {
            throw new NotImplementedException();
        }

        public OrderServiceStatus OrderServiceStatusFindById(int id)
        {
            return _context.OrderServiceStatus.Find(id);
        }

        public bool UpdateEquipment(Equipment equipment)
        {
            var result = _context.Equipment.SingleOrDefault(p => p.id.Equals(equipment.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(equipment);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        public bool UpdateOs(OrderService orderService)
        {
            var result = _context.OrderService.SingleOrDefault(p => p.id.Equals(orderService.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(orderService);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
