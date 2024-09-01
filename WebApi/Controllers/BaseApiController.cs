using Common.Enums;
using Common.Exceptions;
using Common.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;

        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

        protected IActionResult Handle<T>(ResponseWrapper<T> response)
        {
            return response.IsSuccessful ? Ok(response) : StatusCode(response.InternalError.StatusCode, response);
        }
    }
}