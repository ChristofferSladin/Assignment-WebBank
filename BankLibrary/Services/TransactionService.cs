using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using System.Text;
using static Assignment_WebBank.Services.ITransactionService;
using static Assignment_WebBank.Services.TransactionService;
using BankLibrary;

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

        public ErrorCode Transfer(int accountId, int toAccountId, decimal amount)
        {
            var fromAccountDb = _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .FirstOrDefault();

            var toAccountDb = _dbContext.Accounts
                .Where(a => a.AccountId == toAccountId)
                .FirstOrDefault();

            if (fromAccountDb == null || toAccountDb == null)
            {
                return ErrorCode.AccountNotFound;
            }

            if (amount <= 0 || amount > fromAccountDb.Balance)
            {
                return ErrorCode.IncorrectAmount;
            }

            fromAccountDb.Balance -= amount;
            toAccountDb.Balance += amount;

            var debitTransaction = new Transaction
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Operation = "Transfer Withhdraw",
                Type = "Credit",
                Amount = amount * -1,
                Balance = fromAccountDb.Balance
            };

            var creditTransaction = new Transaction
            {
                AccountId = toAccountId,
                Date = DateTime.Now,
                Operation = "Transfer Deposit",
                Type = "Credit",
                Amount = amount,
                Balance = toAccountDb.Balance
            };

            _dbContext.Transactions.Add(debitTransaction);
            _dbContext.Transactions.Add(creditTransaction);
            _dbContext.SaveChanges();

            return ErrorCode.OK;
        }

        public List<TransactionVM> GetTransactions(int accountId)
        {
            return _dbContext.Transactions
                .Where(a => a.AccountId == accountId)
                .OrderByDescending(a => a.Date)
                .OrderByDescending(a => a.TransactionId)
                .Select(a => new TransactionVM
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

        public List<AccountVM> GetAccounts(int accountId)
        {
            return _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .OrderByDescending(a => a.Created)
                .Select(a => new AccountVM
                {
                    AccountId = accountId,
                    Balance = a.Balance,
                    CreatedDate = a.Created,
                }).ToList();
        }

        public AccountVM GetOneAccount(int accountId)
        {
            var accountDb = _dbContext.Accounts
                .Where(a => a.AccountId == accountId)
                .Select(a => new AccountVM
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
