using Domain.Customer;

namespace Application.Customers
{
    public interface ICustomerRepository
    {
        void Add(Customer customer);
        Task<bool> CustomerAlreadyExistsAsync(string email);
    }
}
