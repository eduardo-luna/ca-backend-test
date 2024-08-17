using Shared.Exceptions;

namespace Domain.Customer.Exceptions
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(string message) : base(message) { }
    }
}
