using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TecnicController : Controller
    {
        private IResponseApiService _response;
        private IEmployeeBusiness _business;
        private ITokenService _token;

        public TecnicController(IResponseApiService response, IEmployeeBusiness business, ITokenService token)
        {
            _response = response;
            _business = business;
            _token = token;
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

            List<Employee> employee = _business.GetListTecnic(order, limit, offset, search.ToUpper(), sort);


            Response.Headers.Add("X-Total-Count", _business.CountListEmployee(search.ToUpper()).ToString());
            return Ok(_response.ResponseApi(0, employee));

        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("ListTenicSelect")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ListTecnicSelect()
        {
            return Ok(_response.ResponseApi(0, _business.ListTecnicSelect()));
        }

        [HttpGet]
        [Produces("application/json")]
        [Route("Teste")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> getInfoOsByTecnic()
        {
            return Ok(_response.ResponseApi(0, _business.getInfoOsByTecnic(3)));
        }


    }
}
