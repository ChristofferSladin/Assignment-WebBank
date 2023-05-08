using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
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

        public bool IsSuspiciousActivity(List<Transaction> transactions)
        {
            var totalAmount = transactions.Where(t => t.Date >= DateTime.Now.AddHours(-72))
                                           .Sum(t => t.Amount);

            if (totalAmount > 15000)
            {
                return true;
            }

            return false;
        }

        public void CheckForMoneyLaundering()
        {
            var suspiciousTransactions = new List<string>();

            foreach (var country in _dbContext.Customers.Select(c => c.Country).Distinct())
            {
                Console.WriteLine($"Checking for suspicious activity in {country}...");
                var customers = _dbContext.Customers.Where(c => c.Country == country).ToList();
                CheckForSuspiciousActivity(customers, suspiciousTransactions);
                Console.WriteLine($"Finished checking for suspicious activity in {country}.");
            }

            Console.WriteLine("Generating report...");
            GenerateReport(suspiciousTransactions);
        }

        private void CheckForSuspiciousActivity(List<Customer> customers, List<string> suspiciousTransactions)
        {
            foreach (var customer in customers)
            {
                var accounts = _dbContext.Accounts.Where(a => a.AccountId == customer.CustomerId).ToList();
                CheckForSuspiciousActivityInAccounts(customer, accounts, suspiciousTransactions);
            }
        }

        private void CheckForSuspiciousActivityInAccounts(Customer customer, List<Account> accounts, List<string> suspiciousTransactions)
        {
            foreach (var account in accounts)
            {
                var transactions = _dbContext.Transactions.Where(t => t.AccountId == account.AccountId).ToList();
                CheckForSuspiciousTransaction(customer, account, transactions, suspiciousTransactions);
                CheckForSuspiciousTotalAmount(customer, account, transactions, suspiciousTransactions);
            }
        }

        private void CheckForSuspiciousTransaction(Customer customer, Account account, List<Transaction> transactions, List<string> suspiciousTransactions)
        {
            foreach (var transaction in transactions)
            {
                if (IsSuspiciousActivity(transactions))
                {
                    suspiciousTransactions.Add($"Suspicious Transaction; Customer: {customer.Givenname + customer.Surname}, Account: {account.AccountId}, Transaction: {transaction.TransactionId}");
                }
            }
        }

        private void CheckForSuspiciousTotalAmount(Customer customer, Account account, List<Transaction> transactions, List<string> suspiciousTransactions)
        {
            if (IsSuspiciousActivity(transactions))
            {
                suspiciousTransactions.Add($"Customer: {customer.Givenname + customer.Surname}, Account: {account.AccountId}, Total Amount: {transactions.Sum(t => t.Amount)}");
            }
        }

        private void GenerateReport(List<string> suspiciousTransactions)
        {
            var reportPath = $"{Directory.GetCurrentDirectory()}\\SuspiciousTransactionsReport.txt";

            using (var writer = new StreamWriter(reportPath))
            {
                foreach (var transaction in suspiciousTransactions)
                {
                    writer.WriteLine(transaction);
                }
            }

            Console.WriteLine($"Report generated: {reportPath}");
        }




    }
}
