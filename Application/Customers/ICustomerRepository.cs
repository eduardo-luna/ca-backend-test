using Domain.Customer;

namespace Application.Customers
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        Task<bool> CustomerAlreadyExistsAsync(string email);
        Task<Customer?> GetByIdAsync(Guid id);
        Task<List<Customer>> GetAllAsync();
    }
}
