using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using BankLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BankAppDataContext _dbContext;

        public AccountController(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;

        }

        // READ ONE ACCOUNT ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ONE Account and the transactions from the database
        /// </summary>
        /// <returns>
        /// One Account and the transactions
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Account{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned ONE Account with the transactions
        /// </response>

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<AccountDTO>> GetOne(int id, int limit, int offset)
        {
            var account = _dbContext.Accounts.Include(t => t.Transactions).FirstOrDefault(a => a.AccountId == id);

            if (account == null)
            {
                return BadRequest("Account not found");
            }

            var transactions = account.Transactions
                .OrderByDescending(t => t.Date)
                .Skip(offset)
                .Take(limit)
                .Select(t => new TransactionDTO
                {
                    TransactionId = t.TransactionId,
                    Amount = t.Amount,
                    Date = t.Date,
                    Operation = t.Operation,
                    Balance = t.Balance,
                })
                .ToList();

            var accountModel = new AccountDTO
            {
              AccountId = account.AccountId,
              Balance = account.Balance,
              CreatedDate = account.Created,
              Transactions = transactions,
            };

            return Ok(accountModel);
        }
    }
}
