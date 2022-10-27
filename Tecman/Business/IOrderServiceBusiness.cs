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

        public bool CompleteOrderService(OrderServiceComplete orderServiceComplete);

        List<OrderService> GetListOrderService(String sortDirection, int limit, int offset, String search, String sort);
        int CountListOrderService(String search);
    }
}
