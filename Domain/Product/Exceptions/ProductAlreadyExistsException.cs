using Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Product.Exceptions
{
    public sealed class ProductAlreadyExistsException : BadRequestException
    {
        public ProductAlreadyExistsException(string name) : base($"A product with name {name} alerady exists") { }
    }
}
