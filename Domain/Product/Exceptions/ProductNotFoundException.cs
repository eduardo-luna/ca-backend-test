using Shared.Exceptions;

namespace Domain.Product.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(string message) : base(message) { }
    }
}
