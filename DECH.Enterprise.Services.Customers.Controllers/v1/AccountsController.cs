using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using DECH.Enterprise.Services.Customers.Controllers.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DECH.Enterprise.Services.Customers.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IMapper _mapper;

        public AccountsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("Preferences")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountsQueryResponse))]
        public IActionResult GetPreferences([FromQuery] AccountsQueryRequest parameters)
        {
            if (!ModelState.IsValid) return BadRequest();

            AccountsQuery request = _mapper.Map<AccountsQuery>(parameters);

            return Ok(request);
        }

        //[HttpGet("{rocket}")]
        //public IActionResult GetRocket(
        //[FromRoute] String rocket,
        //[FromQuery] RocketQueryModel query)
        //{
        //    if (!ModelState.IsValid) return BadRequest();


        //    return Ok($"Rocket {rocket} launched to {query.Planet} using {Enum.GetName(typeof(FuelTypeEnum), query.FuelType)} fuel type");
        //}

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
