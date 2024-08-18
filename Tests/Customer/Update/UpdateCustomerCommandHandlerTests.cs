using Application.Customers;
using Application;
using Moq;
using Domain.Customer.Exceptions;
using Application.Customers.Update;

namespace Tests.Customer.Update
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenNameIsNullOrEmpty()
        {
            //Arrange
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "", "Eduardo@Nexer.com", "rua 1, n 2");

            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenEmailIsNullOrEmpty()
        {
            //Arrange
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "Eduardo", "", "rua 1, n 2");

            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenAddressIsNullOrEmpty()
        {
            //Arrange
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "Eduardo", "Eduardo@Nexer.com", "");

            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_WhenCustomerDoesntExistsInDatabase()
        {
            //Arrange
            var command = new UpdateCustomerCommand(Guid.NewGuid(), "Eduardo", "Eduardo@Nexer.com", "Rua 1, N 2");

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowAlreadyExistsException_WhenEmailExistsInDatabase()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var command = new UpdateCustomerCommand(guid, "Eduardo", "Eduardo@Nexer.com", "Rua 1, N 2");
            var customer = new Domain.Customer.Customer { Id = guid, Name = "Eduardo", Email = "EduardoLuna@Nexer.com", Address = "Rua 1, n 2" };

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);
            _customerRepositoryMock.Setup(x => x.CustomerAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerAlreadyExistsException>(() => handler.Handle(command, default));
        }

        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var command = new UpdateCustomerCommand(guid, "Eduardo", "Eduardo@Nexer.com", "Rua 1, N 2");
            var customer = new Domain.Customer.Customer { Id = guid, Name = "Eduardo", Email = "Eduardo@Nexer.com", Address = "Rua 1, n 2" };

            _customerRepositoryMock.Setup(x => x.CustomerAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);

            var handler = new UpdateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }

    }
}
