﻿using MediatR;

namespace Application.Products.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}
