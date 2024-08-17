using Shared.Exceptions;

namespace Domain.Customer.Exceptions
{
    public sealed class CustomerAlreadyExistsException : BadRequestException
    {
        public CustomerAlreadyExistsException(string email) : base($"customer with email {email} already exists in database") { }
    }
}
