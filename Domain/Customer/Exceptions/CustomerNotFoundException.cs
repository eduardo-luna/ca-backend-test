using Shared.Exceptions;

namespace Domain.Customer.Exceptions
{
    public sealed class CustomerNotFoundException : NotFoundException
    {
        public CustomerNotFoundException(Guid id) : base($"customer with id {id} was not found") { }
    }
}
