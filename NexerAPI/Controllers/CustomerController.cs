using Application.Customers;
using Application.Customers.Create;
using Application.Customers.Delete;
using Application.Customers.GetById;
using Application.Customers.GetAll;
using Application.Customers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NexerAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command, ISender sender)
        {
            await sender.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerRequest request, ISender sender)
        {
            var command = new UpdateCustomerCommand(id, request.Name, request.Email, request.Address);
            await sender.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, ISender sender)
        {
            var command = new DeleteCustomerCommand(id);
            await sender.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<CustomerDto> Get(int id, ISender sender)
        {
            var query = new GetCustomerByIdQuery(id);
            return await sender.Send(query);
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetAll(ISender sender)
        {
            return await sender.Send(new GetAllCustomersQuery());
        }
    }
}
