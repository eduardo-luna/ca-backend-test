using Domain.Common;

namespace Domain.Product
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
    }
}
