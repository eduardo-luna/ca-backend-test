using MediatR;

namespace Application.Products.Update
{
    public record UpdateProductCommand(Guid Id, string Name) : IRequest;
}
