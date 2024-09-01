using Application.Features.AccountHolders.Commands;
using Application.Features.AccountHolders.Queries;
using Common.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountHolderController : BaseApiController
    {
        [HttpPost("add")]
        public async Task<IActionResult> CreateAccountHolder([FromBody] CreateAccountHolder createAccountHolder)
        {
            var response = await Sender.Send(new CreateAccountHolderCommand
                { CreateAccountHolder = createAccountHolder });
            return Handle(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccountHolder([FromBody] UpdateAccountHolder updateAccountHolder)
        {
            var response = await Sender.Send(new UpdateAccountHolderCommand
                { UpdateAccountHolder = updateAccountHolder });
            return Handle(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccountHolder(int id)
        {
            var response = await Sender.Send(new DeleteAccountHolderCommand
                { Id = id });
            return Handle(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountHolderById(int id)
        {
            var response = await Sender.Send(new GetAccountHolderByIdQuery
                { Id = id });
            return Handle(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAccountHolders()
        {
            var response = await Sender.Send(new GetAccountHoldersQuery());
            return Handle(response);
        }

        [HttpGet("allPaging")]
        public async Task<IActionResult> GetAccountHoldersPaging([FromQuery(Name = "page")] int page = 1,
            [FromQuery(Name = "size")] int size = 10
        )
        {
            var response = await Sender.Send(new GetAccountHoldersPagingQuery
            {
                page = page, size = size
            });
            return Handle(response);
        }
    }
}