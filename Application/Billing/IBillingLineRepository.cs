using Domain.BillingLines;

namespace Application.Billing
{
    public interface IBillingLineRepository
    {
        void Add(BillingLine billingLine);
    }
}
