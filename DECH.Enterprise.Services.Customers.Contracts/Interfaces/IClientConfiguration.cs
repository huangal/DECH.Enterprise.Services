using System;

namespace DECH.Enterprise.Services.Customers.Contracts.Interfaces
{
    public interface IClientConfiguration
    {
        string ClientName { get; set; }
        DateTime InvokedDateTime { get; set; }
    }

}
