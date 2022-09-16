using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class EmployeeController : Controller
    {

        private IResponseApiService _response;
        private IEmployeeBusiness _business;
        private ITokenService _token;

        public EmployeeController(IResponseApiService response, IEmployeeBusiness business, ITokenService token)
        {
            _response = response;
            _business = business;
            _token = token;
        }


        [HttpPost]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(EmployeeCreate employeeCreate)
        {
            bool create = _business.Create(employeeCreate);

            if (create == false) return BadRequest(create);

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpPatch]
        [Authorize("Bearer")]
        [Route("{id}/disable-employee")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DisableUser(int id)
        {

            Employee employee = _business.FindById(id);

            if (employee == null) return BadRequest(_response.ResponseApi(100, null));

            bool disable = _business.DisableEmployee(employee);

            if (!disable) return BadRequest(_response.ResponseApi(-1, null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(Employee))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> FindById(int id)
        {

            Employee employee = _business.FindById(id);

            if (employee == null) return BadRequest(_response.ResponseApi(100, null));

            return Ok(_response.ResponseApi(0, employee));

        }

        [HttpPatch]
        [Authorize("Bearer")]
        [Route("{id}/edit-address-employee")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateAddressEmployee(int id, AddressObject addressObject)
        {

            Employee employee = _business.FindById(id);

            if (employee == null) return BadRequest(_response.ResponseApi(100, null));

            bool update = _business.UpdateAddressEmployee(employee, addressObject);

            if (!update) return BadRequest(_response.ResponseApi(-1, null));

            return Ok(_response.ResponseApi(0, null));

        }
    }

 }
