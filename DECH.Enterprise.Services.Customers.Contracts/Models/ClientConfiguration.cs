using System;
using DECH.Enterprise.Services.Customers.Contracts.Interfaces;

namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
    public class ClientConfiguration : IClientConfiguration
    {
        public string ClientName { get; set; }
        public DateTime InvokedDateTime { get; set; }
    }


    public class ApiTransactionId : IApiTransactionId
    {
        public Guid OperationId { get; set; }
        public Guid TransactionId { get; set; }
    }



}
