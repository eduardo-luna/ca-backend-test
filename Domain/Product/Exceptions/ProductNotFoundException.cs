using Shared.Exceptions;

namespace Domain.Product.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(int id) : base($"product with id {id} does not exists") { }
    }
}
