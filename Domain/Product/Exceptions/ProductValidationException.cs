using Shared.Exceptions;

namespace Domain.Product.Exceptions
{
    public sealed class ProductValidationException : BadRequestException
    {
        public ProductValidationException(string[] errors) : base("Error validating product information", errors)
        {
        }
    }
}
