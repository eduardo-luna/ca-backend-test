using Domain.Common;

namespace Domain.BillingLines
{
    public class BillingLines : BaseEntity
    {
        public Guid ProductId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }
        public int SubTotal { get; set; }
    }
}
