using Application;
using Application.Customers;
using Application.Customers.Create;
using Domain.Customer.Exceptions;
using Moq;

namespace Tests.Customer.Create
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateCustomerCommandHandlerTests()
        {
            _customerRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenNameIsNullOrEmpty()
        {
            //Arrange
            var command = new CreateCustomerCommand("", "Eduardo@Nexer.com", "rua 1, n 2");

            var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenEmailIsNullOrEmpty()
        {
            //Arrange
            var command = new CreateCustomerCommand("Eduardo", "", "rua 1, n 2");

            var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenAddressIsNullOrEmpty()
        {
            //Arrange
            var command = new CreateCustomerCommand("Eduardo", "Eduardo@Nexer.com", "");

            var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowAlreadyExistsException_WhenEmailExistsInDatabase()
        {
            //Arrange
            var command = new CreateCustomerCommand("Eduardo", "Eduardo@Nexer.com", "Rua 1, N 2");

            _customerRepositoryMock.Setup(x => x.CustomerAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerAlreadyExistsException>(() => handler.Handle(command, default));
        }

        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var command = new CreateCustomerCommand("Eduardo", "Eduardo@Nexer.com", "Rua 1, N 2");

            _customerRepositoryMock.Setup(x => x.CustomerAlreadyExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var handler = new CreateCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }
    }
}
