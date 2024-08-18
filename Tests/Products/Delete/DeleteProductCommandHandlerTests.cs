using Application;
using Application.Products;
using Application.Products.Delete;
using Domain.Product;
using Domain.Product.Exceptions;
using Moq;

namespace Tests.Products.Delete
{
    public class DeleteProductCommandHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public DeleteProductCommandHandlerTests()
        {
            _productRepositoryMock = new();
            _unitOfWorkMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_WhenProductDoesntExistsInDatabase()
        {
            //Arrange
            var command = new DeleteProductCommand(1);

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(value: null);

            var handler = new DeleteProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(command, default));
        }

        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var command = new DeleteProductCommand(1);
            var product = new Product { Id = 1, Name = "ProdutoUm" };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);

            var handler = new DeleteProductCommandHandler(_productRepositoryMock.Object, _unitOfWorkMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }
    }
}
