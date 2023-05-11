using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
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
                return ErrorCode.BalanceTooLow;
            }

            fromAccountDb.Balance -= amount;
            toAccountDb.Balance += amount;

            var debitTransaction = new Transaction
            {
                AccountId = accountId,
                Date = DateTime.Now,
                Operation = $"Transfer to account nr: {toAccountDb.AccountId}",
                Type = "Credit",
                Amount = amount * -1,
                Balance = fromAccountDb.Balance
            };

            var creditTransaction = new Transaction
            {
                AccountId = toAccountId,
                Date = DateTime.Now,
                Operation = $"Transfer from account nr: {fromAccountDb.AccountId}",
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

        public async Task CheckForSuspiciousActivity()
        {
            string[] countries = new string[] { "Sweden", "Norway", "Denmark", "Finland" };

            foreach (string country in countries)
            {
                Console.WriteLine($"Scanning customers in {country}...");

                var customers = await _dbContext.Customers
                    .Where(c => c.Country == country)
                    .ToListAsync();

                foreach (var customer in customers)
                {
                    var dispositions = await _dbContext.Dispositions
                        .Include(d => d.Account)
                        .ThenInclude(a => a.Transactions)
                        .Where(d => d.CustomerId == customer.CustomerId)
                        .ToListAsync();

                    foreach (var disposition in dispositions)
                    {
                        var account = disposition.Account;

                        var suspiciousTransactions = account.Transactions
                            .Where(t => t.Amount > 15000
                            || account.Transactions
                            .Where(t2 => t2.Date > DateTime.Now.AddDays(-3))
                            .Sum(t2 => t2.Amount) > 23000);

                        if (suspiciousTransactions.Any())
                        {
                            GenerateReport(country, customer, account, suspiciousTransactions);
                        }
                    }
                }

                Console.WriteLine($"Finished scanning customers in {country}.");
            }
        }
        private void GenerateReport(string country, Customer customer, Account account, IEnumerable<Transaction> suspiciousTransactions)
        {
            string reportDirectory = Path.Combine(@"C:\Reports", country);
            string reportFileName = $"Report{customer.CustomerId}{account.AccountId}_{DateTime.Now.ToShortDateString()}.txt";
            string reportFullPath = Path.Combine(reportDirectory, reportFileName);

            Directory.CreateDirectory(reportDirectory);

            StringBuilder reportData = new StringBuilder();
            reportData.AppendLine($"Report for Customer ID: {customer.CustomerId}, Account ID: {account.AccountId}");
            reportData.AppendLine($"Report Date: {DateTime.Now}");
            reportData.AppendLine("---------------------------------------------------------");
            reportData.AppendLine("Suspicious Transactions:");
            foreach (var transaction in suspiciousTransactions)
            {
                reportData.AppendLine($"Transaction ID: {transaction.TransactionId}, Date: {transaction.Date}, Amount: {transaction.Amount}");
            }

            File.WriteAllText(reportFullPath, reportData.ToString());
        }
    }
}
