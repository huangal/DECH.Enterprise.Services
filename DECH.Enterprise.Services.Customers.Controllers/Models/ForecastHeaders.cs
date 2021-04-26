using System;
using Microsoft.AspNetCore.Mvc;

namespace DECH.Enterprise.Services.Customers.Controllers.Models
{
   

    public class ForecastHeaders
    {
        [FromHeader]
        public string City { get; set; }

        [FromHeader]
        public int TemperatureC { get; set; }

        [FromHeader]
        public string Description { get; set; }

        [FromQuery]
        public string Sorting { get; set; }
    }
}
