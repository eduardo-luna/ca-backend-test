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
        public DbSet<BillingLine> BillingLines { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().HasData(
                    new Customer
                    {
                        Id = Guid.Parse("12081264-5645-407a-ae37-78d5da96fe59"),
                        Name = "Cliente Exemplo 1",
                        Email = "cliente1@example.com",
                        Address =  "Rua Exemplo 1, 123"
                    }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = Guid.Parse("48c6dc20-a943-4f8f-83ca-1e1cf094a683"),
                    Name = "Produto 1"
                },
                new Product
                {
                    Id = Guid.Parse("48c6dc20-a943-4f8f-83ca-1e1cf094a612"),
                    Name = "Produto 2"
                }
                );
        }
    }
}
