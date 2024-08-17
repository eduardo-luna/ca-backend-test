using Application.Customers;
using Domain.Customer;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Add(Customer customer)
        {
            _context.Add(customer);
        }

        public async Task<bool> CustomerAlreadyExistsAsync(string email)
        {
            return await _context.Customers.AnyAsync(x => x.Email == email);
        }
    }
}
