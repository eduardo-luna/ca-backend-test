using Domain.Customer.Exceptions;
using MediatR;

namespace Application.Customers.Update
{
    internal class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateCustomer(request);
            if (errors.Length > 0) 
            {
                throw new CustomerValidationException(errors);
            }

            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if (customer is null)
            {
                throw new CustomerNotFoundException(request.Id);
            }

            if(!request.Email.Trim().ToLower().Equals(customer.Email) &&
                await _customerRepository.CustomerAlreadyExistsAsync(request.Email.Trim().ToLower()))
            {
                throw new CustomerAlreadyExistsException(request.Email);
            }

            customer.Name = request.Name;
            customer.Email = request.Email.Trim().ToLower();
            customer.Address = request.Address;

            _customerRepository.Update(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static string[] ValidateCustomer(UpdateCustomerCommand customer)
        {
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
