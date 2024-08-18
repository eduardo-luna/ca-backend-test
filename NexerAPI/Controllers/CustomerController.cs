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
        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <param name="command"></param>
        /// <returns>204 No Content</returns>
        /// <response code="204">If customer is created</response>
        /// <response code="400">If customer is not created</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command, ISender sender)
        {
            await sender.Send(command);
            return Ok();
        }

        /// <summary>
        /// Updates a customer
        /// </summary>
        /// <param name="id">Customer Guid</param>
        /// <param name="request"></param>
        /// <returns>204 No Content</returns>
        /// <response code="204">If customer is updated</response>
        /// <response code="404">If customer is not found</response>
        /// <response code="400">If customer is not updated</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateCustomerRequest request, ISender sender)
        {
            var command = new UpdateCustomerCommand(id, request.Name, request.Email, request.Address);
            await sender.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a customer
        /// </summary>
        /// <param name="id">Customer Guid</param>
        /// <returns>204 No Content</returns>
        /// <response code="204">If customer is deleted</response>
        /// <response code="404">If customer is not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, ISender sender)
        {
            var command = new DeleteCustomerCommand(id);
            await sender.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Finds a customer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Returns the customer object</returns>
        /// <response code="200">If found</response>
        /// <response code="404">If not found</response>
        [HttpGet("{id}")]
        public async Task<CustomerDto> Get(Guid id, ISender sender)
        {
            var query = new GetCustomerByIdQuery(id);
            return await sender.Send(query);
        }

        /// <summary>
        /// Lists all customers
        /// </summary>
        /// <returns>All customers in database</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<IEnumerable<CustomerDto>> GetAll(ISender sender)
        {
            return await sender.Send(new GetAllCustomersQuery());
        }
    }
}
