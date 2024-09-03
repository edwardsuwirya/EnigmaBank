using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class LogActionFilter(ILogger<LogActionFilter> logger) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var controllerName = context.ActionDescriptor.DisplayName;
        var stopwatch = Stopwatch.StartNew();
        // logger.LogInformation("Executing action method {0} with parameters: {1}", controllerName, arg);
        logger.LogInformation("Executing action method {0}", controllerName);
        // foreach (var parameter in context.ActionArguments)
        // {
        //     Console.WriteLine("{0}", parameter);
        // }

        await next();
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        var message = $"Action {controllerName} took {elapsedMilliseconds} ms to execute.";
        logger.LogInformation(message);
    }
}