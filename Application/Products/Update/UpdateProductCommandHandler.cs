using Domain.Product.Exceptions;
using MediatR;

namespace Application.Products.Update
{
    internal class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var errors = ValidateProduct(request);
            if (errors.Length > 0) { 
                throw new ProductValidationException(errors);
            }

            var product = await _productRepository.GetByIdAsync(request.Id);
            if(product is null)
            {
                throw new ProductNotFoundException(request.Id);
            }

            if(!product.Name.Trim().ToLower().Equals(request.Name.Trim().ToLower()) &&
                await _productRepository.ProductExistsAsync(request.Name))
            {
                throw new ProductAlreadyExistsException(request.Name);
            }

            product.Name = request.Name;

            _productRepository.Update(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static string[] ValidateProduct(UpdateProductCommand product)
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
