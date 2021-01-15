﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DECH.Enterprise.Services.Customers.Contracts.Interfaces;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DECH.Enterprise.Services.Customers.Controllers.v1
{
    /// <summary>
    /// Customers Services provide customer information for banking
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("v{version:apiVersion}/[controller]")]
    public class CustomersController : ControllerBase
    {
        private ICustomerService _dataService;
        //private readonly IWeatherManager _weatherManager;
        private readonly IClientConfiguration _clientConfiguration;
        private ILogger _logger;
        private readonly IServiceDataProtection _dataProtection;


        public CustomersController(ICustomerService dataRepositoryService, 
            IClientConfiguration clientConfiguration, ILogger<CustomersController> logger, IServiceDataProtection dataProtection)
        {
            _dataService = dataRepositoryService;
            //_weatherManager = weatherManager;
            _clientConfiguration = clientConfiguration;
            _logger = logger;
            _dataProtection = dataProtection;
        }

        /// <summary>
        /// Get List of Customers
        /// </summary>
        /// <returns>List of Cutomer objects</returns>
        //[HttpGet]
        //[Authorize(PolicyType.PartnerAccess)]
        //public async Task<IActionResult> GetCustomers()
        //{
        //    var customers = await _dataService.GetCustomersAsync();
        //    if (!customers.Any()) return NotFound();
        //    else return Ok(customers);

        //}

        /// <summary>
        /// Get List of Customers with pagination filter.  Page size and page number.
        /// Default 20
        /// </summary>
        /// <returns>List of Cutomer objects</returns>
        [HttpGet]
        //[Authorize(PolicyType.PartnerAccess)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerModel>))]
        public async Task<IActionResult> GetCustomers([FromQuery] PaginationFilter filter)
        {

            var validFilter = new PaginationFilter(filter.Page, filter.Size);

            var customers = await _dataService.GetCustomersAsync(validFilter);

            if (!customers.Any()) return NotFound();
            else return Ok(customers);

        }



        /// <summary>
        /// Get List of Customers with pagination filter.  Page size and page number.
        /// Default 20
        /// </summary>
        /// <returns>List of Cutomer objects</returns>
        [HttpGet("List")]
        //[Authorize(PolicyType.PartnerAccess)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomersResponse))]
        public async Task<IActionResult> GetCustomerList([FromQuery] PaginationFilter filter)
        {

            var validFilter = new PaginationFilter(filter.Page, filter.Size);

            var response = await _dataService.GetCustomerListAsync(validFilter);

            return response.Total <= 0 ? NotFound() : (IActionResult)Ok(response);

        }


        /// <summary>
        /// Get a single customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Customers/25
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _dataService.GetCustomersAsync(id);
                return customer != null ? Ok(customer) : (IActionResult)NotFound(new Status
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = "Not Found",
                    Description = "An internal server error has occurred.  Please, try later."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error with formatter");
                //string error = communication.Serialize();
                //_logger.LogError( "Error Creating Contact {0}", communication.Priority);
                //todo:
               // _logger.LogException(ex, $"Error Getting Customer \"{id}\"");

                // _logger.LogError($"{ex.ToString().ParseToPlain()}");
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

        }


        /// <summary>
        /// Create a new customer
        /// </summary>
        /// <remarks>
        /// Sample request: This crate a new **Customer**
        ///
        ///     POST api/Customers
        ///         {
        ///             "name": "Peter",
        ///             "last": "Doe",
        ///             "age": 45,
        ///             "email": "user@example.com",
        ///             "product": "VISA"
        ///         }
        ///
        /// </remarks>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        // [ModelValidation]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
        public async Task<IActionResult> CreateCustomer([FromBody] CustomerModel customer)
        {
            try
            {
                var clientName = _clientConfiguration.ClientName;

                var result = await _dataService.CreateCustomerAsync(customer);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Status { Code = 500, Message = "Internal Server Error", Description = ex.ToString() });
            }

        }


        /// <summary>
        /// Update customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/Customers/25
        ///     {
        ///       "id": 25,
        ///       "name": "Peter",
        ///       "last": "Doe",
        ///       "age": 45,
        ///       "email": "Peter.Dow@example.com",
        ///       "product": "VISA"
        ///      }
        ///
        /// </remarks>
        /// <param name="customer"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        // [ModelValidation]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerModel))]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerModel customer, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (customer.Id != id) return BadRequest();
            var response = await _dataService.GetCustomersAsync(id);
            if (response == null) return NotFound();

            var result = _dataService.UpdateCustomerAsync(customer);

            return Ok(result.Result);
        }

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE api/Customers/25
        ///     
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">Delete has been successfully processed</response>
        [HttpDelete("{id}")]
        // [ModelValidation]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var response = await _dataService.GetCustomersAsync(id);
                if (response == null) return NotFound();

                var result = await _dataService.DeleteCustomerAsync(id);
                if (!result) return Content(StatusCodes.Status406NotAcceptable.ToString(), "Unable to process your request.");

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Deleting");

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        /// <summary>
        /// Get a requested number of customer
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     GET api/Customers/List/10
        ///     
        /// </remarks>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("List/{count}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerModel>))]
        public async Task<IActionResult> GetList(int count)
        {
            var customers = await _dataService.GetCustomerListAsync(count);
            if (!customers.Any()) return NotFound();
            else return Ok(customers);

        }


        /// <summary>
        /// Get total number of customers
        /// </summary>
        /// <returns></returns>
        [HttpGet("Count")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public IActionResult GetCount()
        {
            var count = _dataService.GetCustomersCount();
            return Ok(count);
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("Search")]
        public async Task<IActionResult> Search(string fragment)
        {
            var result = await _dataService.GetByPartialName(fragment);
            if (!result.Any())
            {
                return NotFound(fragment);
            }
            return Ok(result);
        }


        //[HttpPost("Decode")]
        //public IActionResult Decode([FromBody] string data)
        //{
        //    string encrypted = "CfDJ8K3aWspf8pRNgo2HuquMzZl9kKzeSoZUILUFo90ADVzPmf5lJm7xhvvOIjtUZ_-D6ZYELZgj4iup_nPP-Stv6-OxJ4upTPrLL3_wwu7s2OPK298s2YoPoORhK70WYbbuRgP4PUWzO1ebRCgpX9J7j7zuji7xgE3tT0zJMfQX4mlU";


        //    string decrypted;

        //    var isDectypted = _dataProtection.TryDecrypt(encrypted, out decrypted);

        //    if (isDectypted) return Ok(decrypted);
        //    return Unauthorized();


        //    //var result = _dataProtection.Decrypt(data);
        //    //return Ok(result);
        //}

    }
}
