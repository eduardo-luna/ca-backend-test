using Shared.Exceptions;

namespace Domain.Billing.Exceptions
{
    public class EmptyBillingLineException : BadRequestException
    {
        public EmptyBillingLineException() : base("The billing line items object is empty")
        {
            
        }
    }
}
