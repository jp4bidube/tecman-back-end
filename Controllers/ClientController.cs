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
using Tecman.ValueObject.ClientObjects;
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ClientController : Controller
    {
        private IResponseApiService _response;
        private IAddressService _address;
        private IClientBusiness _business;

        public ClientController(IResponseApiService response, IClientBusiness business,IAddressService address)
        {
            _response = response;
            _business = business;
            _address = address;
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

            List<Client> employee = _business.GetListClient(order, limit, offset, search.ToUpper(), sort);


            Response.Headers.Add("X-Total-Count", _business.CountListClient(search.ToUpper()).ToString());
            return Ok(_response.ResponseApi(0, employee));

        }


        [HttpPut]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("{id}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(int id , ClientUpdate clientUpdate)
        {
            Client client = _business.Find(id);

            if (client == null) return BadRequest(_response.ResponseApi(-200, null));

            bool update = _business.Update(client, clientUpdate);

            if (!update) return BadRequest(_response.ResponseApi(-1, null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpPatch]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("{id}/set-default-address")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> setDefaultAddress(int id, ClientAddressObj clientAddressObj)
        {
            Client client = _business.Find(id);

            if (client == null) return BadRequest(_response.ResponseApi(-200, null));

            ClientAddress clientAddress = _business.GetClientAddress(id, clientAddressObj.addressId);

            if (clientAddress == null) return BadRequest(_response.ResponseApi(-300, null));

            bool setDefault = _business.SetDefault(clientAddress);

            if (!setDefault) return BadRequest(_response.ResponseApi(-2, null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpPut]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("client-address/{addressId}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> editClientAddress(int addressId, ClientAddressUpdate clientAddressUpdate)
        {
            Address address = _address.findById(addressId);

            if (address == null) return BadRequest(_response.ResponseApi(-300, null));

            bool update = _address.UpdateClientAddress(address,clientAddressUpdate);

            if (update == false) return BadRequest(_response.ResponseApi(-2, null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("client-address/{clientId}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> creatClientAddress(int clientId, AddressObject clientAddressUpdate)
        {
            Address address = _address.Create(new Address {
                street = clientAddressUpdate.street,
                cep = clientAddressUpdate.cep,
                district = clientAddressUpdate.district,
                number = clientAddressUpdate.number,
                complement = clientAddressUpdate.complement
                 });

            if (address == null) return BadRequest(_response.ResponseApi(-2, null));

            ClientAddress clientAddress = _business.CreateClientAddress(address.id, clientId);

            if (clientAddress == null) return BadRequest(_response.ResponseApi(-2, null));

            if (clientAddressUpdate.defaultAddress == true)
            {
                bool setDefault = _business.SetDefault(clientAddress);

                if (!setDefault) return BadRequest(_response.ResponseApi(-2, null));
            }

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(ClientCreate clientCreate)
        {

            ClientUnique client = _business.FindByCPF(clientCreate.cpf);


            if(clientCreate.cpf != null) { 
            if (client != null) return BadRequest(_response.ResponseApi(-201, null));
            }

            bool create = _business.Create(clientCreate);

            if (create == false) return BadRequest(_response.ResponseApi(-2, null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("{id}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> FindById(int id)
        {
            ClientUnique client = _business.FindById(id);

            if (client == null) return BadRequest(_response.ResponseApi(100, null));

            return Ok(_response.ResponseApi(0, client));

        }

        [HttpGet]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [Route("cpf/{cpf}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> FindByCPF(string cpf)
        {
            ClientUnique client = _business.FindByCPF(cpf);

            if (client == null) return BadRequest(_response.ResponseApi(100, null));

            return Ok(_response.ResponseApi(0, client));

        }
    }
}
