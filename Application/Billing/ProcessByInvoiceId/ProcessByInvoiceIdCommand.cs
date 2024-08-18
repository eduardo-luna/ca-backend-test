using MediatR;

namespace Application.Billing.ProcessByInvoiceId
{
    public record ProcessByInvoiceIdCommand(int Id) : IRequest;
}
