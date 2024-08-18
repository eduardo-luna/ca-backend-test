using Shared.Exceptions;

namespace Domain.Billing.Exceptions
{
    public class MissingProductException : BadRequestException
    {
        public MissingProductException(string[] missingIds) : base("One or more products were not found, please create missing products", missingIds)
        {
            
        }
    }
}
