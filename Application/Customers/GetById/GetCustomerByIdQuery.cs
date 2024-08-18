using MediatR;

namespace Application.Customers.GetById
{
    public record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
}
