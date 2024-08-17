using MediatR;

namespace Application.Customers.Update
{
    public record UpdateCustomerCommand(int Id, string Name, string Email, string Address) : IRequest;
}
