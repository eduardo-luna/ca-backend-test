using MediatR;

namespace Application.Customers.GetAll
{
    public record GetAllCustomersQuery() : IRequest<IEnumerable<CustomerDto>>;
}
