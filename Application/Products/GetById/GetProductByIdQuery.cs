using MediatR;

namespace Application.Products.GetById
{
    public record GetProductByIdQuery(int Id) : IRequest<ProductDto>;
}
