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
            return _context.OrderService.Where(prop => prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)).Count();
        }

        public int CountListOrderServiceByClient(string q, int clientId)
        {
            return _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId)).Count();
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

        public List<OrderService> GetListOrderServiceByClientIdOrderById(string sortDirection, int limit, int offset, string q, int clientId)
        {
            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId))
                    .OrderByDescending(prop => prop.id)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId))
                    .OrderBy(prop => prop.id)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
        }

        public List<OrderService> GetListOrderServiceByClientIdOrderByStatus(string sortDirection, int limit, int offset, string q, int clientId)
        {
            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId))
                    .OrderByDescending(prop => prop.orderServiceStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId))
                    .OrderBy(prop => prop.orderServiceStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
        }
        public List<OrderService> GetListOrderServiceByClientIdOrderByDateCreated(string sortDirection, int limit, int offset, string q, int clientId)
        {
            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId))
                    .OrderByDescending(prop => prop.dateCreated)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.dateCreated.ToString().Contains(q)) && prop.client.id.Equals(clientId))
                    .OrderBy(prop => prop.dateCreated)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
        }
        public List<OrderService> GetListOrderServiceByEndereco(string sortDirection, int limit, int offset, string q)
        {

            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderByDescending(prop => prop.street)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderBy(prop => prop.street)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
        }

        public List<OrderService> GetListOrderServiceByStatus(string sortDirection, int limit, int offset, string q)
        {

            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderByDescending(prop => prop.orderServiceStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderBy(prop => prop.orderServiceStatus.status)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
        }

        public List<OrderService> GetListOrderServiceByTelefone(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderByDescending(prop => prop.client.phoneNumber)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderBy(prop => prop.client.phoneNumber)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
        }

        public List<OrderService> GetListOrderServiceOrderById(string sortDirection, int limit, int offset, string q)
        {
            if (sortDirection == "desc")
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderByDescending(prop => prop.id)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
            else
            {
                List<OrderService> orderService = _context.OrderService.Where(prop => (prop.id.ToString().Contains(q) || prop.client.phoneNumber.ToUpper().Contains(q) || prop.orderServiceStatus.status.ToUpper().Contains(q) || prop.street.ToUpper().Contains(q)))
                    .OrderBy(prop => prop.id)
                    .Skip((offset * limit))
                    .Take(limit)
                    .ToList();
                return orderService;
            }
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

        public List<TechnicalVisit> getVisitWarrantyByEquipmentId(int equipmentId)
        {
            return _context.TechnicalVisit.Where(prop => prop.Equipment.id.Equals(equipmentId)).OrderByDescending(prop => prop.dateVisit).ToList();
        }

        public bool CreateVisit(TechnicalVisit technicalVisit)
        {
            try
            {
                _context.Add(technicalVisit);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}
