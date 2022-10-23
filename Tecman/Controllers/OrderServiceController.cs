using Microsoft.AspNetCore.Authorization;
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

            bool create = _business.Create(orderServiceCreate);

            if (!create) return BadRequest(_response.ResponseApi(301,null));

            return Ok(_response.ResponseApi(0, null));

        }

    }
}
