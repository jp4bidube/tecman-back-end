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
using Tecman.ValueObject.EmployeeObjects;

namespace Tecman.Controllers
{
  [ApiVersion("1")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class HealthController : Controller
  {

    public HealthController()
    { }

    [HttpGet]
    [Route("Health")]
    [ProducesResponseType((200), Type = typeof(ApiMessage))]
    [ProducesResponseType((400), Type = typeof(ApiMessage))]
    [ProducesResponseType(401)]
    [ProducesResponseType(408)]
    public IActionResult Health(string token)
    {
      return Ok(true);
    }

  }
}
