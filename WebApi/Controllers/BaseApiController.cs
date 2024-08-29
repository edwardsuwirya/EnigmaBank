using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender;

        public ISender Sender => _sender ??= HttpContext.RequestServices.GetService<ISender>();
    }
}