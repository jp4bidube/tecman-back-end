using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.EmployeeObjects;
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

        public bool CompleteOrderService(OrderServiceComplete orderServiceComplete)
        {
            OrderService Os = _repository.FindById(orderServiceComplete.id);

            Os.pieceSold = orderServiceComplete.pieceSold;
            Os.serviceExecuted = orderServiceComplete.serviceExecuted;
            Os.tecnic = _employee.FindById(orderServiceComplete.tecnicId);
            Os.datePayment = orderServiceComplete.datePayment;
            Os.clientPiece = orderServiceComplete.clientPiece;
            Os.amountReceived = orderServiceComplete.amountReceived;
            Os.budget = orderServiceComplete.budget;
            Os.orderServiceStatus = _repository.OrderServiceStatusFindById(2);

            bool updateOs = _repository.UpdateOs(Os);

            if (!updateOs) return false;

            foreach(EquipmentUnique equip in orderServiceComplete.equipments)
            {
                Equipment equipment = _repository.FindEquipmentById(equip.id);

                equipment.warrantyPeriod = equip.warrantyPeriod;
                equipment.mounthsWarranty = equip.mounthsWarranty;

                bool updateEquip = _repository.UpdateEquipment(equipment);
            }

            return true;


        }

        public int CountListOrderService(string search)
        {
            return _repository.CountListOrderService(search);

        }

        public int CountListOrderServiceByClient(string search, int clientId)
        {
            return _repository.CountListOrderServiceByClient(search, clientId);

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
                periodAttendance = orderServiceCreate.periodAttendance,     
                                
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

        public bool CreateVisit(VisitCreate visitCreate)
        {
            TechnicalVisit technicalVisit = new TechnicalVisit
            {
                clientePiece = visitCreate.clientePiece,
                dateVisit = visitCreate.dateVisit,
                Employee = _employee.FindById(visitCreate.employeeId),
                Equipment = _repository.FindEquipmentById(visitCreate.equipmentId),
                serviceExecuted = visitCreate.serviceExecuted
            };

            return _repository.CreateVisit(technicalVisit);
        }

        public bool Find(int id)
        {
            if (_repository.FindById(id) == null) return false;

            return true;

        }

        public OrderServiceUnique FindById(int id)
        {
            OrderService order = _repository.FindById(id);
            if (order == null) return null;

            OrderServiceUnique OsUnique = new OrderServiceUnique
            {
                id = id,
                absence1 = order.absence1,
                absence2 = order.absence2,
                amountReceived = order.amountReceived,
                budget = order.budget,
                cep = order.cep,
                client = order.client,
                clientPiece = order.clientPiece,
                complement = order.complement,
                createdBy = order.createdBy.name,
                dateCreated = order.dateCreated,
                datePayment = order.datePayment,
                defect = order.defect,
                device_qtd = order.device_qtd,
                district = order.district,
                equipments = null,
                number = order.number,
                observacao = order.observacao,
                orderServiceStatus = order.orderServiceStatus,
                pieceSold = order.pieceSold,
                serviceExecuted = order.serviceExecuted,
                street = order.street,
                periodAttendance = order.periodAttendance,
                Tecnic = new TecnicListSelect
                {
                    id = order?.tecnic?.id,
                    name = order?.tecnic?.name,
                }
            };

            List<Equipment> equipments = _repository.getAllEquipmentsByOS(id);

            List<EquipmentUnique> equipmentsUnique = new List<EquipmentUnique> { };

            if(equipments.Count > 0) { 

            foreach (Equipment equip in equipments)
            {
                EquipmentUnique equipmentUniqueEach = new EquipmentUnique
                {
                    id = equip.id,
                    brand = equip.brand,
                    model = equip.model,
                    type = equip.equipment,
                    mounthsWarranty = equip.mounthsWarranty,
                    warrantyPeriod = equip.warrantyPeriod,
                };

                equipmentsUnique.Add(equipmentUniqueEach);
            }

            OsUnique.equipments = equipmentsUnique;
            }

            return OsUnique;

        }

        public List<OrderService> GetListOrderService(string sortDirection, int limit, int offset, string search, string sort)
        {
            switch (sort)
            {
                case "os":
                    return _repository.GetListOrderServiceOrderById(sortDirection, limit, offset, search);
                    break;
                case "endereco":
                    return _repository.GetListOrderServiceByEndereco(sortDirection, limit, offset, search);
                    break;
                case "telefone":
                    return _repository.GetListOrderServiceByTelefone(sortDirection, limit, offset, search);
                    break;
                case "status":
                    return _repository.GetListOrderServiceByStatus(sortDirection, limit, offset, search);
                    break;
                default:
                    return _repository.GetListOrderServiceOrderById(sortDirection, limit, offset, search);
                    break;

            }

        }

        public List<OrderService> GetListOrderServiceByClientId(string sortDirection, int limit, int offset, string search, string sort, int clientId)
        {
            switch (sort)
            {
                case "os":
                    return _repository.GetListOrderServiceByClientIdOrderById(sortDirection, limit, offset, search, clientId);
                    break;
                case "status":
                    return _repository.GetListOrderServiceByClientIdOrderByStatus(sortDirection, limit, offset, search, clientId);
                    break;
                case "dataCriacao":
                    return _repository.GetListOrderServiceByClientIdOrderByDateCreated(sortDirection, limit, offset, search, clientId);
                    break;
                default:
                    return _repository.GetListOrderServiceByClientIdOrderById(sortDirection, limit, offset, search, clientId);
                    break;

            }

        }

        public List<TechnicalVisit> getVisitWarrantyByEquipmentId(int equipmentId)
        {
            return _repository.getVisitWarrantyByEquipmentId(equipmentId);
        }

        public bool UpdateOS(OrderServicePutObject orderServicePutObject)
        {
            OrderService os = _repository.FindById(orderServicePutObject.id);

            if (os == null) return false;

            os.number = orderServicePutObject.number;
            os.observacao = orderServicePutObject.observacao;
            os.pieceSold = orderServicePutObject.pieceSold;
            os.serviceExecuted = orderServicePutObject.serviceExecuted;
            os.street = orderServicePutObject.street;
            os.tecnic = _employee.FindById(orderServicePutObject.tecnicId);
            os.district = orderServicePutObject.district;
            os.defect = orderServicePutObject.defect;
            os.datePayment = orderServicePutObject.datePayment;
            os.complement = orderServicePutObject.complement;
            os.clientPiece = orderServicePutObject.clientPiece;
            os.client = _client.FindById(orderServicePutObject.clientId);
            os.cep = orderServicePutObject.cep;
            os.budget = orderServicePutObject.budget;
            os.amountReceived = orderServicePutObject.amountReceived;
            os.periodAttendance = orderServicePutObject.periodAttendance;

            bool update = _repository.UpdateOs(os);

            if (!update) return false;

            Equipment equipment = _repository.FindEquipmentById(orderServicePutObject.device.id);

            if (equipment == null) return true;

            equipment.brand = orderServicePutObject.device.brand;
            equipment.equipment = orderServicePutObject.device.type;    
            equipment.model = orderServicePutObject.device.model;
            equipment.warrantyPeriod = orderServicePutObject.device.warrantyPeriod;
            equipment.mounthsWarranty = orderServicePutObject.device.mounthsWarranty;


            bool updateEquip = _repository.UpdateEquipment(equipment);

            return updateEquip;
        }
    }
}
