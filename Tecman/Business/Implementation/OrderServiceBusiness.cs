using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;

namespace Tecman.Business.Implementation
{
    public class OrderServiceBusiness : IOrderServiceBusiness
    {

        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private IResponseApiService _response;
        private IOrderServiceRepository _repository;
        private IClientRepository _client;
        private IAddressService _address;
        private IEmployeeRepository _employee;


        public OrderServiceBusiness(IResponseApiService response, IOrderServiceRepository repository, IClientRepository client, IAddressService address, IEmployeeRepository employee)
        {
            _response = response;
            _repository = repository;
            _client = client;
            _address = address;
            _employee = employee;

        }

        public bool Create(OrderServiceCreate orderServiceCreate)
        {

        OrderService orderService = new OrderService
            {
                client = _client.FindById(orderServiceCreate.clientId),
                createdBy = orderServiceCreate.userLogged,
                defect = orderServiceCreate.defect,
                tecnic = _employee.FindById(orderServiceCreate.tecnicId),
                street = orderServiceCreate.street,
                number = orderServiceCreate.number,
                cep = orderServiceCreate.cep,
                complement = orderServiceCreate.complement,
                district = orderServiceCreate.district,
                dateCreated = DateTime.Now,
                observacao = orderServiceCreate.observacao,
                orderServiceStatus = _repository.OrderServiceStatusFindById(1),
                device_qtd = orderServiceCreate.devices.Length,            
            };



            return _repository.Create(orderService);
        }
    }
}
