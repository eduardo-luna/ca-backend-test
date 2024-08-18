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
        var command = new DeleteCustomerCommand(Guid.NewGuid());

        _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

        var handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act


        //Act & Assert
        await Assert.ThrowsAsync<CustomerNotFoundException>(() => handler.Handle(command, default));
    }

    [Fact]
    public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
    {
        //Arrange
        var guid = Guid.NewGuid();  
        var command = new DeleteCustomerCommand(guid);
        var customer = new Domain.Customer.Customer { Id = guid, Name = "Eduardo", Email = "EduardoLuna@Nexer.com", Address = "Rua 1, n 2" };

        _customerRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(customer);

        var handler = new DeleteCustomerCommandHandler(_customerRepositoryMock.Object, _unitOfWorkMock.Object);

        //Act


        //Act & Assert
        Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
    }
}
