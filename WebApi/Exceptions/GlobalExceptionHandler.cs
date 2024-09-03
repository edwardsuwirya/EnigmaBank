using Common.Exceptions;
using Common.Wrapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace WebApi.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Title = "An error occurred while processing your request",
            Detail = exception.Message,
        };
        Log.Error(exception.Message);
        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var resp = ResponseWrapper<string>.Fail(GeneralErrors.General(exception.Message));
        await httpContext.Response
            .WriteAsJsonAsync(resp, cancellationToken);

        return true;
    }
}