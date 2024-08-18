using Application.Billing.ProcessByInvoiceId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NexerAPI.Controllers
{
    [Route("api/billing")]
    [ApiController]
    public class BillingController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, ISender sender)
        {
            await sender.Send(new ProcessByInvoiceIdCommand(id));
            return Ok("Invoice imported successfully");
        }
    }
}
