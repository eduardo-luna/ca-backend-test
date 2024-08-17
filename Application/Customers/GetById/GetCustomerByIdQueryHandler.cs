using MediatR;
using Domain.Customer.Exceptions;
using Domain.Customer;

namespace Application.Customers.GetById
{
    internal class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDto> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if(customer is null)
            {
                throw new CustomerNotFoundException(request.Id);
            }

            return new CustomerDto(customer.Id, customer.Name, customer.Email, customer.Address);
        }
    }
}
