using Common.Wrapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;

        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

        protected IActionResult GenerateResponse<T>(ResponseWrapper<T> response)
        {
            return response.Match(onSuccess: () => Ok(response),
                onFailure: response.Error);
        }
    }
}