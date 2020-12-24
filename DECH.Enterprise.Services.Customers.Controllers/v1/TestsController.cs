using System;
using Microsoft.AspNetCore.Mvc;

namespace DECH.Enterprise.Services.Customers.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TestsController : ControllerBase
    {


        [HttpGet]
        public IActionResult Get()
        {

            return Ok("Good Results");
        }


    }
}
