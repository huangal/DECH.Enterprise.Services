
namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
    // [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PhoneType
    {
        Home,
        Work,
        Cell
    }

    public enum PriorityType
    {
        Low,
        Medium,
        High
    }
}
