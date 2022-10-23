using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.ValueObject;

namespace Tecman.Business
{
    public interface IOrderServiceBusiness
    {
        public bool Create(OrderServiceCreate orderServiceCreate);
    }
}
