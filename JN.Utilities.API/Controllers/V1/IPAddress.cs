using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JN.Utilities.API.Controllers.V1
{
    /// <summary>
    /// What is my IP Address?
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IpAddress : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            var ip = HttpContext.Connection.RemoteIpAddress.ToString();

            return Ok(ip);
        }
    }
}
