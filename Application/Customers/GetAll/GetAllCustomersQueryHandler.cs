using MediatR;

namespace Application.Customers.GetAll
{
    internal class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
    {
        private readonly ICustomerRepository _customerRepository;

        public GetAllCustomersQueryHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _customerRepository.GetAllAsync();
            var dto = customers.Select(x => new CustomerDto(x.Id, x.Name, x.Email, x.Address));

            return dto;
        }
    }
}
