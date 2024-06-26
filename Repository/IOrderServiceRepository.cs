﻿using System;
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
        public List<Equipment> getAllEquipmentsByOS(int id);
        Equipment FindEquipmentById(int? id);
        bool UpdateOs(OrderService orderService);
        bool UpdateEquipment(Equipment equipment);
        public int CountListOrderService(string search);
        public List<TechnicalVisit> getVisitWarrantyByEquipmentId(int equipmentId);
        public int CountListOrderServiceByClient(string search, int clientId);
        List<OrderService> GetListOrderServiceOrderById(String sortDirection, int limit, int offset, String q);
        List<OrderService> GetListOrderServiceByEndereco(String sortDirection, int limit, int offset, String q);
        List<OrderService> GetListOrderServiceByTelefone(String sortDirection, int limit, int offset, String q);
        List<OrderService> GetListOrderServiceByStatus(String sortDirection, int limit, int offset, String q);
        List<OrderService> GetListOrderServiceByClientIdOrderByDateCreated(string sortDirection, int limit, int offset, string q, int clientId);
        List<OrderService> GetListOrderServiceByClientIdOrderByStatus(string sortDirection, int limit, int offset, string q, int clientId);
        List<OrderService> GetListOrderServiceByClientIdOrderById(string sortDirection, int limit, int offset, string q, int clientId);

        bool CreateVisit(TechnicalVisit technicalVisit);
    }
}
