using Domain.Billing;

namespace Application.Billing
{
    public interface IBillingRepository
    {
        Task<Domain.Billing.Billing?> GetByInvoiceNumber(string invoiceNumber);
        Task<bool> InvoiceNumberExistsAsync(string invoiceNumber);
        void Add(Domain.Billing.Billing billing);
    }
}
