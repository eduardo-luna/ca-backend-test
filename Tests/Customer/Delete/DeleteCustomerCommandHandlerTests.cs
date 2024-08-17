using Application;
using Application.Customers;
using Application.Customers.Delete;
using Domain.Customer.Exceptions;
using Moq;

namespace Tests.Customer.Delete;

public class DeleteCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customerRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteCustomerCommandHandlerTests()
    {
        _customerRepositoryMock = new();
        _unitOfWorkMock = new();
    }

    [Fact]
    public async Task Handle_Should_ThrowNotFoundException_WhenCustomerDoesntExistsInDatabase()
    {
        //Arrange
        var command = new DeleteCustomerCommand(1);

        _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(value: null);

        var handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act


        //Act & Assert
        await Assert.ThrowsAsync<CustomerNotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
    {
        //Arrange
        var command = new DeleteCustomerCommand(1);
        var customer = new Domain.Customer.Customer { Id = 1, Name = "Eduardo", Email = "EduardoLuna@Nexer.com", Address = "Rua 1, n 2" };

        _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);

        var handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act


        //Act & Assert
        Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
    }
}
