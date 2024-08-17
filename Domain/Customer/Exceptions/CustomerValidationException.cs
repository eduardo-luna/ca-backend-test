using Shared.Exceptions;

namespace Domain.Customer.Exceptions
{
    public sealed class CustomerValidationException : BadRequestException
    {
        public CustomerValidationException(string[] errors) : base("Error validating customer information", errors) { 
            
        }
    }
}
