using System;
using System.Collections.Generic;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using Swashbuckle.AspNetCore.Filters;

namespace DECH.Enterprise.Services.Customers.Contracts.Examples
{
    public class CustomerModelExample : IExamplesProvider<CustomerModel>
    {
        public CustomerModel GetExamples()
        {
            return new CustomerModel
            {
                Id = 104,
                Age = 25,
                Email = "john.leen@gamil.com",
                Last = "Leen",
                Name = "John",
                Product = "Visa Card"
            };
        }
    }



    public class CustomerModelExamples : IExamplesProvider<IEnumerable<CustomerModel>>
    {
        public IEnumerable<CustomerModel> GetExamples()
        {
            return new List<CustomerModel>
            {
                new CustomerModel
                {
                     Id = 104,
                    Age = 25,
                    Email = "peter.gool@hotmail.com",
                    Last = "Gool",
                    Name = "Peter",
                    Product = "Master Card"

                },
                new CustomerModel
                {
                     Id = 104,
                    Age = 25,
                    Email = "lili.Brown@gmail.com",
                    Last = "Brown",
                    Name = "Lili",
                    Product = "Discovery Card"

                },
                new CustomerModel
                {
                     Id = 104,
                    Age = 25,
                    Email = "john.leen@aol.com",
                    Last = "Leen",
                    Name = "John",
                    Product = "Visa Card"

                }
            };
        }
    }
}
