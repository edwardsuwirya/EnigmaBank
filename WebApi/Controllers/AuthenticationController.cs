using Application.Features.UserAccess.Queries;
using Common.Requests;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AuthenticationController : BaseApiController
    {
        [HttpPost("authenticate-user")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthentication userAuthentication)
        {
            var response = await Sender.Send(new AuthenticateUserQuery
                { UserAuthentication = userAuthentication });
            // if (response.IsSuccessful) setTokenCookie(response.Data.Tokens.RefreshToken);
            return Handle(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            var response = await Sender.Send(new RefreshTokenQuery
                { UserRefreshToken = refreshTokenRequest });
            return Handle(response);
        }

        private void setTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}