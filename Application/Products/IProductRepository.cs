using Domain.Product;

namespace Application.Products
{
    public interface IProductRepository
    {
        void Add(Product product);
        Task<bool> ProductExistsAsync(string name);
    }
}
