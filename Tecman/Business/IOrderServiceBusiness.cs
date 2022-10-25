using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.ValueObject;

namespace Tecman.Business
{
    public interface IOrderServiceBusiness
    {
        public OrderService Create(OrderServiceCreate orderServiceCreate);

        public OrderService FindById(int id);
    }
}
