using MediatR;

namespace Application.Customers.Update
{
    public record UpdateCustomerCommand(Guid Id, string Name, string Email, string Address) : IRequest;
}
