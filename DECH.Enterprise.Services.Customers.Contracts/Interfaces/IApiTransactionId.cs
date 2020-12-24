using System;

namespace DECH.Enterprise.Services.Customers.Contracts.Interfaces
{
    public interface IApiTransactionId
    {
        Guid OperationId { get; set; }
    }

}
