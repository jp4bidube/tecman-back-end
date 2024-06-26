﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Tecman.Business;
using Tecman.Models;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.OrderServiceObjects;

namespace Tecman.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrderServiceController : Controller
    {
        private IResponseApiService _response;
        private IOrderServiceBusiness _business;
        private IUserBusiness _user;
        private ITokenService _token;

        public OrderServiceController(IResponseApiService response, IOrderServiceBusiness business, ITokenService token, IUserBusiness user)
        {
            _response = response;
            _business = business;
            _token = token;
            _user = user;
        }



        [HttpPost]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(OrderServiceCreate orderServiceCreate)
        {
            var userLogged = Convert.ToInt64(User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier"))?.Value);

            User user = _user.FindById((int)userLogged);

            orderServiceCreate.userLogged = user.employee;

            OrderService create = _business.Create(orderServiceCreate);

            if (create == null) return BadRequest(_response.ResponseApi(301,null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpPatch]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("{id}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CompleteOrderService(OrderServiceComplete orderServiceComplete, int id)
        {
            orderServiceComplete.id = id;

            bool exist = _business.Find(id);

            if (!exist) return BadRequest(_response.ResponseApi(-150,null));

            return Ok(_business.CompleteOrderService(orderServiceComplete));
        }


        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("{id}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetById(int id)
        {
            bool exist = _business.Find(id);

            if (!exist) return BadRequest(_response.ResponseApi(-150, null));

            return Ok(_response.ResponseApi(0, _business.FindById(id)));
        }


        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get([FromQuery] String sort, [FromQuery] String order, [FromQuery] int limit, [FromQuery] int offset, [FromQuery] String search)
        {
            if (order == null || limit == null || offset == null || sort == null) return BadRequest(_response.ResponseApi(1, null));

            if (search == null) search = "";

            List<OrderService> OS = _business.GetListOrderService(order, limit, offset, search.ToUpper(), sort);


            Response.Headers.Add("X-Total-Count", _business.CountListOrderService(search.ToUpper()).ToString());
            return Ok(_response.ResponseApi(0, OS));

        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Put(OrderServicePutObject orderServicePutObject)
        {
          return Ok(_response.ResponseApi(0, _business.UpdateOS(orderServicePutObject)));
        }

        [HttpPatch]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("absence/{id}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> NotificationAbsence(int id , Absence absence)
        {
            return Ok(_response.ResponseApi(0, _business.NotificationAbsence(absence.date, id)));
        }


        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("cliente/{clientId}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetClient([FromQuery] String sort, [FromQuery] String order, [FromQuery] int limit, [FromQuery] int offset, [FromQuery] String search, int clientId)
        {
            if (order == null || limit == null || offset == null || sort == null) return BadRequest(_response.ResponseApi(1, null));

            if (search == null) search = "";

            List<OrderService> OS = _business.GetListOrderServiceByClientId(order, limit, offset, search.ToUpper(), sort, clientId);


            Response.Headers.Add("X-Total-Count", _business.CountListOrderServiceByClient(search.ToUpper(),clientId).ToString());
            return Ok(_response.ResponseApi(0, OS));

        }


        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("equipment-warrant/{equipmentId}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetEquipmentById(int equipmentId)
        {

            return Ok(_response.ResponseApi(0, _business.getVisitWarrantyByEquipmentId(equipmentId)));
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("CreateVisit")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateVisit(VisitCreate visitCreate)
        {

            return Ok(_response.ResponseApi(0, _business.CreateVisit(visitCreate)));

        }
    }
}
