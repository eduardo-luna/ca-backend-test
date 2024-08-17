using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace Shared.ExceptionsHandler
{
    public class ExceptionsHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            var problemDetails = exception switch
            {
                NotFoundException nf => new ProblemDetails
                {
                    Title = "NotFound",
                    Detail = nf.Message,
                    Status = StatusCodes.Status404NotFound
                },
                BadRequestException br => new ProblemDetails
                {
                    Title = "BadRequest",
                    Detail = br.Message,
                    Status = StatusCodes.Status400BadRequest,
                    Extensions = new Dictionary<string, object?>
                    {
                        { "errors", br.Errors }
                    }
                },
                _ => new ProblemDetails
                {
                    Title = "ServerError",
                    Status = StatusCodes.Status500InternalServerError
                }

            };

            httpContext.Response.StatusCode = problemDetails.Status!.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
