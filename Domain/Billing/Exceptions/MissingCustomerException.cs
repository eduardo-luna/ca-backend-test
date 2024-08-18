using Shared.Exceptions;

namespace Domain.Billing.Exceptions
{
    public class MissingCustomerException : NotFoundException
    {
        public MissingCustomerException(Guid id) : base($"Customer {id} is missing, please create customer")
        {
        }
    }
}
