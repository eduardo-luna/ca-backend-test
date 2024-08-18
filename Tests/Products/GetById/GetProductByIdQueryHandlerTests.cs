using Application.Products;
using Application.Products.GetById;
using Domain.Product;
using Domain.Product.Exceptions;
using Moq;

namespace Tests.Products.GetById
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;

        public GetProductByIdQueryHandlerTests()
        {
            _productRepositoryMock = new();
        }

        [Fact]
        public async Task Handle_Should_ThrowNotFoundException_WhenProductDoesntExistsInDatabase()
        {
            //Arrange
            var command = new GetProductByIdQuery(Guid.NewGuid());

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(value: null);

            var handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object);

            //Act


            //Act & Assert
            await Assert.ThrowsAsync<ProductNotFoundException>(() => handler.Handle(command, default));
        }


        [Fact]
        public void Handle_Should_ReturnSuccess_WhenAllCriteriasAreMet()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var command = new GetProductByIdQuery(guid);
            var product = new Product { Id = guid, Name = "ProdutoUm" };

            _productRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var handler = new GetProductByIdQueryHandler(_productRepositoryMock.Object);

            //Act


            //Act & Assert
            Assert.True(handler.Handle(command, default).IsCompletedSuccessfully);
        }
    }
}
