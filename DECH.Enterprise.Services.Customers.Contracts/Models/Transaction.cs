using System;
using System.Text.Json.Serialization;
using DECH.Enterprise.Services.Customers.Contracts.Converters;

namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
    public class Transaction
    {
        public Guid? TransactionId { get; set; }
        public bool IsValidGuid()
        {
            return (Guid.TryParse(TransactionId.ToString(), out var guid) && guid != Guid.Empty);
        }
    }

    [JsonConverter(typeof(AccountIdConverter))]
    public class AccountId
    {
        public Guid Value { get; set; } = Guid.NewGuid();
    }
}
