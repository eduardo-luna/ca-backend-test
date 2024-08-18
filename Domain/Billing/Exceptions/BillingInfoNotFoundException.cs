using Shared.Exceptions;
namespace Domain.Billing.Exceptions
{
    public class BillingInfoNotFoundException : NotFoundException
    {
        public BillingInfoNotFoundException(int Id) : base($"no information found for billing id {Id}")
        {
            
        }
    }
}
