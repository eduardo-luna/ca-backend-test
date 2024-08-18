using Application;
using Application.Billing;
using Application.Customers;
using Application.Products;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var dbConnection = configuration.GetConnectionString("Postgres");
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(dbConnection));

            //repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBillingRepository, BillingRepository>();
            services.AddScoped<IBillingLineRepository, BillingLineRepository>();

            //billingapi httpclient
            services.AddHttpClient("billingApi", (serviceProvider, httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://65c3b12439055e7482c16bca.mockapi.io/api/v1/");
            });

            return services;
        }
    }
}
