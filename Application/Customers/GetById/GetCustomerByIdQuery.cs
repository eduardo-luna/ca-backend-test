using MediatR;

namespace Application.Customers.GetById
{
    public record GetCustomerByIdQuery(int Id) : IRequest<CustomerDto>;
}
