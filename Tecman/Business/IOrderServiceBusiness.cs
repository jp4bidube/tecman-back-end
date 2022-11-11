using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;
using Tecman.ValueObject.OrderServiceObjects;

namespace Tecman.Business
{
    public interface IOrderServiceBusiness
    {
        public OrderService Create(OrderServiceCreate orderServiceCreate);

        public OrderServiceUnique FindById(int id);
        public bool Find(int id);
        public bool UpdateOS(OrderServicePutObject orderServicePutObject);

        public bool CompleteOrderService(OrderServiceComplete orderServiceComplete);

        List<OrderService> GetListOrderService(String sortDirection, int limit, int offset, String search, String sort);

        List<OrderService> GetListOrderServiceByClientId(string sortDirection, int limit, int offset, string search, string sort, int clientId);

        int CountListOrderService(String search);

        int CountListOrderServiceByClient(string search, int clientId);

        List<TechnicalVisit> getVisitWarrantyByEquipmentId(int equipmentId);

        bool CreateVisit(VisitCreate visitCreate);
    }
}
