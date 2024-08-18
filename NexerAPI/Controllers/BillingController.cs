using Application.Billing.ProcessByInvoiceId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NexerAPI.Controllers
{
    [Route("api/billing")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        /// <summary>
        /// Imports an invoice by Invoice ID
        /// </summary>
        /// <param name="id">ID of the Invoice</param>
        /// <returns>A success message</returns>
        /// <response code="200">If the invoice is imported successfully</response>
        /// <response code="404">If the invoice is not found</response>
        /// <response code="400">If the invoice is invalid</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, ISender sender)
        {
            await sender.Send(new ProcessByInvoiceIdCommand(id));
            return Ok("Invoice imported successfully");
        }
    }
}
