using Application.Products.Create;
using Application.Products.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NexerAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command, ISender sender)
        {
            await sender.Send(command);
            return Ok();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateProductRequest request, ISender sender)
        {
            var command = new UpdateProductCommand(id, request.Name);
            await sender.Send(command);
            return Ok();
        }
    }
}
