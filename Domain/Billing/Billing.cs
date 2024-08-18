using Domain.Common;

namespace Domain.Billing
{
    public class Billing : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public DateOnly DateOnly { get; set; }
        public DateOnly DueDate { get; set; }
        public int TotalAmount { get; set; }
        public string Currency {  get; set; }
    }
}
