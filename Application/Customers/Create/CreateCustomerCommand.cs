using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Create
{
    public record CreateCustomerCommand(string Name, string Email, string Address) : IRequest;
}
