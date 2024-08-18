using Application.Products;
using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.GetAll;
using Application.Products.GetById;
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

        /// <summary>
        /// Creates a product
        /// </summary>
        /// <param name="command"></param>
        /// <returns>204 No Content</returns>
        /// <response code="204">If the product is created</response>
        /// <response code="400">If the product is not created</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command, ISender sender)
        {
            await sender.Send(command);
            return Ok();
        }

        /// <summary>
        /// Updates a product
        /// </summary>
        /// <param name="id">Product Guid</param>
        /// <param name="request"></param>
        /// <returns>204 No Content</returns>
        /// <response code="204">If the product is updated</response>
        /// <response code="400">If the product is not updated</response>
        /// <response code="404">If the product is not found</response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateProductRequest request, ISender sender)
        {
            var command = new UpdateProductCommand(id, request.Name);
            await sender.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id">Product Guid</param>
        /// <returns>204 No Content</returns>
        /// <response code="204">If the product is updated</response>
        /// <response code="404">If the product is not found</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, ISender sender)
        {
            await sender.Send(new DeleteProductCommand(id));
            return NoContent();
        }

        /// <summary>
        /// Get a product by ID
        /// </summary>
        /// <param name="id">Product Guid</param>
        /// <returns>The product object</returns>
        /// <response code="200">If the product is found</response>
        /// <response code="404">If the product is not found</response>
        [HttpGet("{id}")]
        public async Task<ProductDto> Get(Guid id, ISender sender)
        {
            return await sender.Send(new GetProductByIdQuery(id));
        }

        /// <summary>
        /// Lists all products
        /// </summary>
        /// <returns>A list containing all products</returns>
        /// <response code="200"></response>
        [HttpGet]
        public async Task<IEnumerable<ProductDto>> GetAll(ISender sender)
        {
            return await sender.Send(new GetAllProductsQuery());
        }
    }
}
