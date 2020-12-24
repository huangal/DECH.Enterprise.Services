using DECH.Enterprise.Services.Customers.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace DECH.Enterprise.Services.Customers.DataAccess
{
    public class CustomersContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersContext(DbContextOptions<CustomersContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customers");
            modelBuilder.Entity<Customer>().HasKey(p => p.Id);
        }
    }
}
