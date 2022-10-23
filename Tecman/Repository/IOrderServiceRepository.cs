using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.Repository
{
    public interface IOrderServiceRepository
    {
        public bool Create(OrderService order);

        public OrderServiceStatus OrderServiceStatusFindById(int id);
    }
}
