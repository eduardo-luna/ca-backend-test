using Shared.Exceptions;

namespace Domain.Billing.Exceptions
{
    public class InvalidInvoiceException : BadRequestException
    {
        public InvalidInvoiceException() : base("Invoice is invalid")
        {
            
        }
    }
}
