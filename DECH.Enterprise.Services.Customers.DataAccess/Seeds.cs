using System.Collections.Generic;
using System.Linq;
using DECH.Enterprise.Services.Customers.Contracts.Models;
using GenFu;

namespace DECH.Enterprise.Services.Customers.DataAccess
{
    public static class Seeds
    {
        public static void CreateSeedData(this CustomersContext context, int records = 1000)
        {

            if (context.Customers.Any()) return;

            var customers = LoadCustomers(records);

            context.AddRange(customers);
            context.SaveChanges();
        }
        
        private static IEnumerable<Customer> LoadCustomers(int count)
        {
            int id = 1;
            GenFu.GenFu.Configure<Customer>()
                .Fill(x => x.Id, () => { return id++; })
                .Fill(x => x.Name).AsFirstName()
                .Fill(x => x.Last).AsLastName()
                .Fill(x => x.Age).WithinRange(18, 70)
                .Fill(x => x.Email).AsEmailAddress()
                .Fill(x => x.Product).WithRandom(ProductList());

            return GenFu.GenFu.ListOf<Customer>(count);
        }

        private static List<string> ProductList() =>
            new List<string>()
            {
                "Visa Card",
                "Goal Saving Card",
                "Healthcare Card",
                "Rewards Card",
                "Saving Cards"
            };
    }
}
