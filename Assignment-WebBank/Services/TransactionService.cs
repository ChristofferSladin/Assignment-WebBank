using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Data;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static Assignment_WebBank.Services.ITransactionService;
using static Assignment_WebBank.Services.TransactionService;

namespace Assignment_WebBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _dbContext;

        public TransactionService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ErrorCode Withdraw(int accountId, decimal amount)
        {
            var accountDb = _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .First();

            if (accountDb == null)
            {
                return ErrorCode.AccountNotFound;
            }

            if (accountDb.Balance < amount)
            {
                return ErrorCode.BalanceTooLow;
            }

            if (amount < 100 || amount > 10000)
            {
                return ErrorCode.IncorrectAmount;
            }

            accountDb.Balance -= amount;

            var transaction = new Transaction
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Operation = "Withdraw",
                Type = "Credit",
                Amount = amount * -1,
                Balance = accountDb.Balance
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return ErrorCode.OK;
        }

        public ErrorCode Deposit(int accountId, decimal amount)
        {
            var accountDb = _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .First();

            if (accountDb == null)
            {
                return ErrorCode.AccountNotFound;
            }

            if (amount < 100 || amount > 10000)
            {
                return ErrorCode.IncorrectAmount;
            }

            accountDb.Balance += amount;

            var transaction = new Transaction
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Operation = "Deposit",
                Type = "Credit",
                Amount = amount,
                Balance = accountDb.Balance
            };

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();

            return ErrorCode.OK;
        }

        public List<TransactionsModel> GetTransactions(int accountId)
        {
            return _dbContext.Transactions
                .Where(a => a.AccountId == accountId)
                .OrderByDescending(a => a.Date)
                .OrderByDescending(a => a.TransactionId)
                .Select(a => new TransactionsModel
                {
                    AccountId = accountId,
                    TransactionId = a.TransactionId,
                    Balance = a.Balance,
                    Date = a.Date.ToShortDateString(),
                    Bank = a.Bank,
                    Operation = a.Operation,
                    Amount = a.Amount,

                }).ToList();
        }
        public List<TransactionsModel> GetTransactionsShowMore(int pageNo, int accountId)
        {
            if (pageNo == 0) { pageNo = 1; }

            var listOfCars = _dbContext.Accounts
                .Where(d => d.AccountId == accountId)
                .SelectMany(t => t.Transactions)
                .OrderByDescending(t => t.Date)
                .OrderByDescending(t => t.TransactionId)
                .GetPaged(pageNo, 5).Results
                .Select(c => new TransactionsModel
                {
                    AccountId = accountId,
                    TransactionId = c.TransactionId,
                    Balance = c.Balance,
                    Date = c.Date.ToShortDateString(),
                    Bank = c.Bank,
                    Operation = c.Operation,
                    Amount = c.Amount,
                }).ToList();

            return listOfCars;
        }

        public List<AccountModel> GetAccounts(int accountId)
        {
            return _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .OrderByDescending(a => a.Created)
                .Select(a => new AccountModel
                {
                    AccountId = accountId,
                    Balance = a.Balance,
                    CreatedDate = a.Created,
                }).ToList();
        }

        public AccountModel GetOneAccount(int accountId)
        {
            var accountDb = _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .Select(a => new AccountModel
                {
                    Balance = a.Balance,
                    CreatedDate = a.Created,
                    AccountId = accountId,
                })
                .First();

            return accountDb;
        }
    }
}
