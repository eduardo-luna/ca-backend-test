using Application.Customers.Create;
using Application.Customers.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NexerAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        
        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command, ISender sender)
        {
            await sender.Send(command);
            return Created();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerRequest request, ISender sender)
        {
            var command = new UpdateCustomerCommand(id, request.Name, request.Email, request.Address);
            await sender.Send(command);
            return NoContent();
        }
    }
}
