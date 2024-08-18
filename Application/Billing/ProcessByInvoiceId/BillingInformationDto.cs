using System.Text.Json.Serialization;

namespace Application.Billing.ProcessByInvoiceId
{

    public class BillingInformationDto
    {
        [JsonPropertyName("0")]
        public DetailsDto Details { get; set; }
        [JsonPropertyName("createdAt")]
        public string CreatedAt { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("invoiceAmount")]
        public string InvoiceAmount { get; set; }
        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }
        [JsonPropertyName("billingLines")]
        public object? BillingLines { get; set; }
        [JsonPropertyName("invoiceDate")]
        public int InvoiceDate { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
    }

    public class BillingCustomerDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("address")]
        public string Address { get; set; }
    }

    public class BillingLinesDto
    {
        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("unit_price")]
        public int UnitPrice { get; set; }
        [JsonPropertyName("subtotal")]
        public int Subtotal { get; set; }
    }

    public class DetailsDto
    {
        [JsonPropertyName("invoice_number")]
        public string InvoiceNumber { get; set; }
        [JsonPropertyName("customer")]
        public BillingCustomerDto Customer { get; set; }
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("due_date")]
        public string DueDate { get; set; }
        [JsonPropertyName("lines")]
        public IEnumerable<BillingLinesDto?> Lines { get; set; }
        [JsonPropertyName("total_amount")]
        public int TotalAmount { get; set; }
    }

}
