using MediatR;

namespace Application.Products.Create
{
    public record CreateProductCommand(string Name) : IRequest;
}
