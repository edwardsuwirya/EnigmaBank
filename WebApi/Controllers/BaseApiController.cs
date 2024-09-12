using Common.Wrapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;
        private IConfiguration _configuration;

        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();

        protected IConfiguration Configuration =>
            _configuration ??= HttpContext.RequestServices.GetService<IConfiguration>();

        protected IActionResult Handle<T>(ResponseWrapper<T> response)
        {
            return response.IsSuccessful ? Ok(response) : StatusCode(response.InternalError.StatusCode, response);
        }
    }
}