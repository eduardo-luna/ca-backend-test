using MediatR;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand(string Name, string Email, string Address) : IRequest;
}
