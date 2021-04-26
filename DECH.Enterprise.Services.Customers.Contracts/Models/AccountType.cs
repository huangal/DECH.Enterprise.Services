
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using ServiceStack;

namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
   // [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AccountType
    {
        [EnumMember(Value = "DDA-A")]
        DDA,
        [EnumMember(Value = "SAV-B")]
        SAV,
        [EnumMember(Value = "LOC-C")]
        LOCAL
    }

    public enum FuelTypeEnum
    {
        
        Solid,
        Liquid
    }
}
