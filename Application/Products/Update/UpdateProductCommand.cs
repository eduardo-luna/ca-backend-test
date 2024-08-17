using MediatR;

namespace Application.Products.Update
{
    public record UpdateProductCommand(int Id, string Name) : IRequest;
}
