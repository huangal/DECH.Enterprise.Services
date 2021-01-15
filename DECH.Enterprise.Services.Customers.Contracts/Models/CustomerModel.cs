using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
    public class CustomerModel
    {
        /// <summary>
        /// Unique Identifier.  No required if create new customer with POST
        /// </summary>
        [Required] public int Id { get; set; }

        /// <summary>
        /// Customer First Name
        /// </summary>
        [Required] public string Name { get; set; }

        /// <summary>
        /// Customer Last Name
        /// </summary>
        [Required] public string Last { get; set; }

        /// <summary>
        /// Customer age. Age should be 18 years or older
        /// </summary>
        [Range(18, 125, ErrorMessage = "Customer must be at least 18 years old.")]
        [Required]
        //[RegularExpression("/(((0|1)[0-9]|2[0-9]|3[0-1])\\/(0[1-9]|1[0-2])\\/((19|20)\\d\\d))$/", ErrorMessage = "Customer must be at least 18 years old.")]
        public int Age { get; set; }

        /// <summary>
        /// Email address
        /// </summary>
        [Required(ErrorMessage ="Email is required for notifications")]
        [EmailAddress]
        public string Email { get; set; }
        [Required] public string Product { get; set; }
    }



    public class CustomersResponse
    {
        public IEnumerable<CustomerModel> Customers { get; set; }
        public int Total { get; set; }
    }



}
