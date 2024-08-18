using Domain.Product.Exceptions;
using MediatR;

namespace Application.Products.GetById
{
    internal class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            return new ProductDto(product.Id, product.Name);
        }
    }
}
