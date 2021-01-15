using System;
using Microsoft.AspNetCore.Mvc;

namespace DECH.Enterprise.Services.Customers.Controllers.v1
{
    /// <summary>
    /// Testing Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestsController : ControllerBase
    {

        /// <summary>
        /// Test getting succesfull response
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {

            return Ok("Good Results");
        }


    }
}
