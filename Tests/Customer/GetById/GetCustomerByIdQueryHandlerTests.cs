using Application.Customers;
using Application.Customers.GetById;
using Domain.Customer.Exceptions;
using Moq;

namespace Tests.Customer.GetById
{
    public class GetCustomerByIdQueryHandlerTests
    {
        private readonly Mock<ICustomerRepository> _customerRepositoryMock;

        public GetCustomerByIdQueryHandlerTests()
        {
            _customerRepositoryMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_WhenCustomerDoesntExistsInDatabase()
        {
            //Arrange
            var command = new GetCustomerByIdQuery(1);

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(value: null);

            var handler = new GetCustomerByIdQueryHandler(_customerRepositoryMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<CustomerNotFoundException>(() => handler.Handle(command, default));
        }


        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var command = new GetCustomerByIdQuery(1);
            var customer = new Domain.Customer.Customer { Id = 1, Name = "Eduardo", Email = "Eduardo@Nexer.com", Address = "Rua 1, n 2" };

            _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

            var handler = new GetCustomerByIdQueryHandler(_customerRepositoryMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }
    }
}
