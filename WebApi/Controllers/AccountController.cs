using Application.Features.Accounts.Commands;
using Application.Features.Accounts.Queries;
using Common.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AccountController() : BaseApiController
    {
        [HttpPost("add")]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccount createAccount)
        {
            var response = await Sender.Send(new CreateAccountCommand
                { CreateAccount = createAccount });
            return Handle(response);
        }

        [HttpPost("transaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest transactionRequest)
        {
            var response = await Sender.Send(new CreateTransactionCommand()
                { Transaction = transactionRequest });
            return Handle(response);
        }

        [HttpGet("transaction/{accountId:int}")]
        public async Task<IActionResult> GetAccountTransaction(int accountId)
        {
            var response = await Sender.Send(new GetAccountTransactionsQuery()
                { Id = accountId });
            return Handle(response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var response = await Sender.Send(new GetAccountByIdQuery
                { Id = id });
            return Handle(response);
        }


        [HttpGet("account-number/{id}")]
        public async Task<IActionResult> GetAccountHoldersPaging(string id)
        {
            var response = await Sender.Send(new GetAccountByAccountNumberQuery()
            {
                AccountNumber = id
            });
            return Handle(response);
        }

        // [TypeFilter(typeof(LogActionFilter), Arguments = ["sss"])]
        // [ServiceFilter<LogActionFilter>]
        // [TypeFilter(typeof(RequiredKeyFilter))]
        [HttpGet("all")]
        public async Task<IActionResult> GetAccounts()
        {
            // throw new Exception("Not implemented");
            var response = await Sender.Send(new GetAccountsQuery());
            return Handle(response);
        }
    }
}