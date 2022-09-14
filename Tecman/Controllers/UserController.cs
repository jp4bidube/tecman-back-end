/**
 * Created: Daniel Quintal
 * Modified: Daniel Quintal
 * Date: January, 31, 2022

 *
 * Mail controller - FRONT
 * 
 */


using Tecman.Business;
using Tecman.Configurations;
using Tecman.Models;
using Tecman.Repository;
using Tecman.Services;
using Tecman.ValueObject;
using Tecman.ValueObject.NewFolder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tecman.ValueObject.UserObject;
using Microsoft.AspNetCore.Authentication;

namespace Tecman.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UserController : Controller
    {

        private IResponseApiService _response;
        private IUserBusiness _business;
        private ITokenService _token;

        public UserController(IResponseApiService response, IUserBusiness business, ITokenService token)
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
        public async Task<IActionResult> Create(UserCreate userCreate)
        {
            ApiMessage create = _business.Create(userCreate);

            if (create.Success == false) return BadRequest(create);

            return Ok(create);

        }

        [HttpPost]
        [Route("Signin")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(TokenObject))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Signin([FromBody] UserCredentials user)
        {

            var token = _business.ValidateCredentials(user);

            if (token == null) return Unauthorized();

            return Ok(_response.ResponseApi(0, token));

        }


        [HttpGet]
        [Authorize("Bearer")]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(TokenObject))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> FindById(int id)
        {

            User user = _business.FindById(id);

            if (user == null) return BadRequest(_response.ResponseApi(100,null));

            return Ok(_response.ResponseApi(0, user));

        }

        [HttpPatch]
        [Authorize("Bearer")]
        [Route("{id}/disable-user")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(TokenObject))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> DisableUser(int id)
        {

            User user = _business.FindById(id);

            if (user == null) return BadRequest(_response.ResponseApi(100, null));

            bool disable = _business.DisableUser(user);

            if(!disable) return BadRequest(_response.ResponseApi(-1, null));

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpGet]
        [Authorize("Bearer")]
        [Route("Me")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(MeObject))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Me()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token"); // receive token from front

            var auth = _token.GetCrendentials(accessToken); // decode token and get credentials

            User user = _business.FindById(auth.nameid);

            MeObject result = new MeObject(user.username, user.employee.name, user.employee.role.role, user.avatarUrl, user.employee.email);

            return Ok(_response.ResponseApi(0, result));

        }

        [HttpPut]
        [Authorize("Bearer")]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(TokenObject))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(int id)
        {

            User user = _business.FindById(id);

            if (user == null) return BadRequest(_response.ResponseApi(100, null));

            bool disable = _business.DisableUser(user);

            if (!disable) return BadRequest(_response.ResponseApi(-1, null));

            return Ok(_response.ResponseApi(0, null));

        }


        
    }
}
