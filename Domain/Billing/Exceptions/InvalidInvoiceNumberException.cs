using Shared.Exceptions;

namespace Domain.Billing.Exceptions
{
    public class InvalidInvoiceNumberException : BadRequestException
    {
        public InvalidInvoiceNumberException() : base("Invoice number is invalid")
        {
            
        }
    }
}
