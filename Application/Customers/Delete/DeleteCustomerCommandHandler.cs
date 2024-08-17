using MediatR;
using Domain.Customer.Exceptions;

namespace Application.Customers.Delete
{
    internal class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetByIdAsync(request.Id);

            if (customer is null) {
                throw new CustomerNotFoundException(request.Id);
            }

            _customerRepository.Delete(customer);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
