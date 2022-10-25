using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.OrderServiceObjects;

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

        public OrderService Create(OrderServiceCreate orderServiceCreate)
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
                device_qtd = orderServiceCreate.devices.Length.ToString(),     
                                
            };

            OrderService os = _repository.Create(orderService);

            if (os == null) return null;

            try
            {
                foreach (EquipmentOSObject device in orderServiceCreate.devices)
                {
                    Equipment equipment = new Equipment
                    {
                        brand = device.brand,
                        equipment = device.type,
                        model = device.model,
                        orderService = os,
                    };

                    bool equipCreate = _repository.CreateEquipment(equipment);
                }
            }
            catch
            {
                return null;
            }

            return os;
        }

        public OrderService FindById(int id)
        {
            return _repository.FindById(id);
        }
    }
}
