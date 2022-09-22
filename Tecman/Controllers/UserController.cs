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
        private IEmployeeBusiness _employee;
        private ITokenService _token;

        public UserController(IResponseApiService response, IUserBusiness business, ITokenService token, IEmployeeBusiness employee)
        {
            _response = response;
            _business = business;
            _token = token;
            _employee= employee;
        }

        [HttpPost]
        [Produces("application/json")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create(UserCreate userCreate)
        {
            User user = _business.FindByUsername(userCreate.username);

            if (user != null) return BadRequest(_response.ResponseApi(101, null));

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

            if (token == null) return Unauthorized(_response.ResponseApi(401,null));

            return Ok(_response.ResponseApi(0, token));

        }


        [HttpGet]
        [Authorize("Bearer")]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(User))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> FindById(int id)
        {

            User user = _business.FindById(id);

            if (user == null) return BadRequest(_response.ResponseApi(100,null));

            return Ok(_response.ResponseApi(0, user));

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

            MeObject result = new MeObject(user.username, user.employee.name, user.employee.role.role, user.employee.avatarUrl, user.employee.email);

            return Ok(_response.ResponseApi(0, result));

        }

        [HttpPut]
        [Authorize("Bearer")]
        [Route("{id}")]
        [Produces("application/json")]
        [ProducesResponseType((200), Type = typeof(TokenObject))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Update(UserUpdate userUpdate, int id)
        {
            User user = _business.FindById(id);

            bool update = _business.Update(userUpdate,user);

            return Ok(_response.ResponseApi(0, null));

        }

        [HttpGet]
        [Route("Refresh/{token}")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        [ProducesResponseType(408)]
        public IActionResult Refresh(string token)
        {
            // check if exist a payload
            if (token == null)
            {
                return BadRequest(_response.ResponseApi(-2, null));
            }
            else
            {
                //check if refresh token is valid and refresh access token case true
                var Rtoken = _business.ValidateCredentials(Uri.UnescapeDataString(token));
                if (Rtoken == null)
                {
                    return Unauthorized();
                }
                else
                {
                    return Ok(_response.ResponseApi(0, Rtoken));
                }
            }
        }


        [HttpGet]
        [Route("Logout")]
        [Authorize("Bearer")]
        [ProducesResponseType((200), Type = typeof(ApiMessage))]
        [ProducesResponseType((400), Type = typeof(ApiMessage))]
        [ProducesResponseType(401)]
        [ProducesResponseType(408)]
        
        public IActionResult Logout()
        {
            var username = User.Identity.Name;
            bool result = _business.RevokeToken(username);
            if (result != true) return BadRequest(_response.ResponseApi(-2, null));

            return Ok(_response.ResponseApi(0, null));
        }

        [HttpPost]
        [Produces("application/json")]
        [Route("Recovery")]
        [ProducesResponseType((200), Type = typeof(NewPassword))]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RecoveryPassword(RecoveryPassword recoveryPassword)
        {

            Employee employee = _employee.FindByCPF(recoveryPassword.cpf);

            if (employee == null) return BadRequest(_response.ResponseApi(-100, null));

            User user = _business.FindByUsername(recoveryPassword.username);

            if (user == null) return BadRequest(_response.ResponseApi(-100, null));

            if(employee.id != user.employee.id) return BadRequest(_response.ResponseApi(-102, null));

            // Generate Claim, input user guid to JWT
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                //TODO employee.name
                new Claim(JwtRegisteredClaimNames.UniqueName, user.username),
                new Claim(JwtRegisteredClaimNames.NameId, user.id.ToString())
            };

            var accessToken = _token.GenerateAccessToken(claims);

            var newPassword = new NewPassword
            {
                employeeId = employee.id,
                userId = user.id,
                username = user.username,
                recoveryToken = accessToken
            }; 

            return Ok(_response.ResponseApi(0, newPassword));

        }
    }
}
