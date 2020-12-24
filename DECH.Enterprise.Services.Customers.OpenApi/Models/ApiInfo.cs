﻿using System;
namespace DECH.Enterprise.Services.Customers.OpenApi.Models
{
    public class ApiInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string TermsOfService { get; set; }
        public Contact Contact { get; set; }
        public License License { get; set; }
    }
}
