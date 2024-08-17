using Application;
using Moq;
using Application.Products;
using Application.Products.Create;
using Domain.Product.Exceptions;

namespace Tests.Products.Create
{
    public class CreateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CreateProductCommandHandlerTests()
        {
            _productRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenNameIsNullOrEmpty()
        {
            //Arrange
            var command = new CreateProductCommand("");

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowAlreadyExistsException_WhenProductNameExistsInDatabase()
        {
            //Arrange
            var command = new CreateProductCommand("ProdutoUm");

            _productRepositoryMock.Setup(x => x.ProductExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductAlreadyExistsException>(() => handler.Handle(command, default));
        }

        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var command = new CreateProductCommand("ProdutoUm");

            _productRepositoryMock.Setup(x => x.ProductExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var handler = new CreateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }
    }
}
