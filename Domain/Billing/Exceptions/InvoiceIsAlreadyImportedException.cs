using Shared.Exceptions;

namespace Domain.Billing.Exceptions
{
    public class InvoiceIsAlreadyImportedException : BadRequestException
    {
        public InvoiceIsAlreadyImportedException(string invoiceNumber) : base($"Invoice {invoiceNumber} is already imported")
        {
            
        }
    }
}
