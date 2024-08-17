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

        public Task<bool> CustomerAlreadyExistsAsync(string email)
        {
            return _context.Customers.AnyAsync(x => x.Email == email);
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            return _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Customer customer)
        {
            _context.Update(customer);
        }
    }
}
