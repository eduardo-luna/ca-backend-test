using MediatR;

namespace Application.Customers.Delete
{
    public record DeleteCustomerCommand(int Id) : IRequest;
}
