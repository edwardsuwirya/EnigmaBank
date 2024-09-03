using Common.Exceptions;
using Common.Responses;
using Common.Wrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class RequiredKeyFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var resp = ResponseWrapper<string>.Fail(ValidationErrors.General("Model is invalid"));
            context.Result = new BadRequestObjectResult(resp);
        }

        var key = context.HttpContext.Request.Headers["key"];
        if (!key.Equals("123456"))
        {
            var resp = ResponseWrapper<string>.Fail(AuthenticationErrors.InvalidToken());
            context.Result = new UnauthorizedObjectResult(resp);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}