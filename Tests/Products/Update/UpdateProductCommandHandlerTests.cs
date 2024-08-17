using Application;
using Moq;
using Application.Products;
using Domain.Product.Exceptions;
using Application.Products.Update;
using Domain.Product;

namespace Tests.Products.Update
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public UpdateProductCommandHandlerTests()
        {
            _productRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenIdIsInvalid()
        {
            //Arrange
            var command = new UpdateProductCommand(0, "ProdutoUm");

            var handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowValidationException_WhenNameIsNullOrEmpty()
        {
            //Arrange
            var command = new UpdateProductCommand(1, "");

            var handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductValidationException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_WhenProductDoesntExistsInDatabase()
        {
            //Arrange
            var command = new UpdateProductCommand(1, "Eduardo");

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(value: null);

            var handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowAlreadyExistsException_WhenProductNameExistsInDatabase()
        {
            //Arrange
            var command = new UpdateProductCommand(1, "ProdutoDois");
            var product = new Product { Id = 1, Name = "ProdutoUm" };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _productRepositoryMock.Setup(x => x.ProductExistsAsync(It.IsAny<string>())).ReturnsAsync(true);

            var handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductAlreadyExistsException>(() => handler.Handle(command, default));
        }

        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var command = new UpdateProductCommand(1, "ProdutoUm");
            var product = new Product { Id = 1, Name = "ProdutoDois" };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
            _productRepositoryMock.Setup(x => x.ProductExistsAsync(It.IsAny<string>())).ReturnsAsync(false);

            var handler = new UpdateProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }
    }
}
