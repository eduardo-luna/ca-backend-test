using Domain.Product;
using Domain.Product.Exceptions;
using MediatR;

namespace Application.Products.Create
{
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateProduct(request);
            if (errors.Length > 0)
            {
                throw new ProductValidationException(errors);
            }

            if(await _productRepository.ProductExistsAsync(request.Name))
            {
                throw new ProductAlreadyExistsException(request.Name);
            }

            var product = new Product { Name = request.Name };

            _productRepository.Add(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static string[] ValidateProduct(CreateProductCommand product)
        {
            List<string> errors = [];

            if (string.IsNullOrEmpty(product.Name))
            {
                errors.Add("Product name cannot be null or empty");
            }

            return [.. errors];
        }
    }
}
