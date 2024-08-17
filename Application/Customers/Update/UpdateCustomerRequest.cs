using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Customers.Update
{
    public record UpdateCustomerRequest(string Name, string Email, string Address);
}
