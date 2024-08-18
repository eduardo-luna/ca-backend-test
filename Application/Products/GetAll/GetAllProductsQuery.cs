using MediatR;

namespace Application.Products.GetAll
{
    public record GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>;
}
