using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;

namespace Tecman.Repository
{
    public interface IOrderServiceRepository
    {
        public OrderService Create(OrderService order);

        public OrderServiceStatus OrderServiceStatusFindById(int id);

        public bool CreateEquipment(Equipment equipment);

        public OrderService FindById(int id);
    }
}
