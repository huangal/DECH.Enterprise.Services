using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using System.ComponentModel.DataAnnotations;
using ServiceStack;
using System.Collections.Generic;
using DECH.Enterprise.Services.Customers.Contracts;

namespace DECH.Enterprise.Services.Customers.Controllers.Models
{
    public class AccountsQueryRequest : IValidatableObject
    {
        [BindRequired]
        public string AccountId{ get; set; }


        [BindRequired]
        public string TypeAccount { get; set; }

        [BindNever]
        public AccountType AccountType { get; set; }


        [BindNever]
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            AccountType accountType;

            if (!this.TypeAccount.TryParseWithMemberName( out accountType))
            {
                results.Add(new ValidationResult("Invalid Account Type"));

                //yield return new ValidationResult($"Invalid Account Type: {TypeAccount}");
            }
            else
            {
                this.AccountType = accountType;
                 Name = "Account Type is good";
            }

            


            return results;
        }
    }



    public class AccountsQuery
    {
       
        public string AccountId { get; set; }

       public AccountType AccountType { get; set; }

        public string AccountDescription { get; set; }
    }


    public class AccountsQueryResponse
    {
        [BindRequired]
        public string AccountId { get; set; }


        [BindRequired]
        public AccountType AccountType { get; set; }
    }


    public class RocketQueryModel
    {
        [Required]
        [ApiAllowableValues("Name", typeof(FuelTypeEnum))]
        public string FuelType { get; set; }

        [Required]
        public String Planet { get; set; }
    }
}
