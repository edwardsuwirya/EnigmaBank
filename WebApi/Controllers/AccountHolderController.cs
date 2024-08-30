using Application.Features.AccountHolders.Commands;
using Application.Features.AccountHolders.Queries;
using Common.Enums;
using Common.Exceptions;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

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
            return GenerateResponse(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateAccountHolder([FromBody] UpdateAccountHolder updateAccountHolder)
        {
            var response = await Sender.Send(new UpdateAccountHolderCommand
                { UpdateAccountHolder = updateAccountHolder });
            return GenerateResponse(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAccountHolder(int id)
        {
            var response = await Sender.Send(new DeleteAccountHolderCommand
                { Id = id });
            return GenerateResponse(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountHolderById(int id)
        {
            var response = await Sender.Send(new GetAccountHolderByIdQuery
                { Id = id });
            return GenerateResponse(response);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAccountHolders()
        {
            var response = await Sender.Send(new GetAccountHoldersQuery());
            return GenerateResponse(response);
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
            return GenerateResponse(response);
        }
    }
}