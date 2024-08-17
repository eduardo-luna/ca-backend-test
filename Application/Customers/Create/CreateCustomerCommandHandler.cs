using Domain.Customer;
using Domain.Customer.Exceptions;
using MediatR;

namespace Application.Customers.Create
{
    internal class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateCustomer(request);
            if (errors.Length > 0)
            {
                throw new CustomerValidationException(errors);
            }

            if(await _customerRepository.CustomerAlreadyExistsAsync(request.Email))
            {
                throw new CustomerAlreadyExistsException(request.Email);
            }

            var customer = new Customer
            {
                Name = request.Name,
                Email = request.Email,
                Address = request.Address
            };

            _customerRepository.Add(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static string[] ValidateCustomer(CreateCustomerCommand customer) { 
            List<string> errors = [];

            if (string.IsNullOrEmpty(customer.Name))
            {
                errors.Add("Name cannot be null or empty");
            }

            if (string.IsNullOrEmpty(customer.Email))
            {
                errors.Add("Email cannot be null or empty");
            }

            if (string.IsNullOrEmpty(customer.Address))
            {
                errors.Add("Address cannot be null or empty");
            }

            return [.. errors];
        }

    }
}
