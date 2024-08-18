using Domain.Common;

namespace Domain.Billing
{
    public class Billing : BaseEntity
    {
        public string InvoiceNumber { get; set; }
        public Guid CustomerId { get; set; }
        public DateOnly Date { get; set; }
        public DateOnly DueDate { get; set; }
        public int TotalAmount { get; set; }
        public string Currency {  get; set; }
    }
}
