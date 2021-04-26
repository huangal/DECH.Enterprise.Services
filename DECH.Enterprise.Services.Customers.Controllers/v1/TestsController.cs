using System;
using DECH.Enterprise.Services.Customers.Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DECH.Enterprise.Services.Customers.Controllers.v1
{
    /// <summary>
    /// Testing Controller
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
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


        [HttpGet("Operation")]
        public IActionResult GetOperation([FromHeader(Name = "x-my-Name")] string name)
        {

            return Ok(name);
        }

        // POST: weatherForecast/multipleHeaders
        [HttpPost("multipleHeaders")]
        public IActionResult Post([FromHeader] ForecastHeaders forecastHeaders)
        {
            try
            {
                Console.WriteLine($"Got a forecast for city: {forecastHeaders.City}," +
                                    $"temperature: {forecastHeaders.TemperatureC} and" +
                                    $"description: {forecastHeaders.Description}!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return new AcceptedResult();
        }
    }
}
