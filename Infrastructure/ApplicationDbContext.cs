using Domain.Billing;
using Domain.BillingLines;
using Domain.Customer;
using Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Billing> Billing { get; set; }
        public DbSet<BillingLines> BillingLines { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
