namespace DECH.Enterprise.Services.Customers.Contracts.Models
{
    public class Customer
    {
        private string email;

        public int Id { get; set; }
        public string Name { get; set; }
        public string Last { get; set; }
        public int Age { get; set; }

        public string Email { get => email; set => email = GetFixedEmailAddress(value); }
        public string Product { get; set; }

        private string GetFixedEmailAddress(string value) =>
            !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Last) ? $"{Name}.{Last}{value.Substring(value.IndexOf("@"))}" : value;
    }

}
