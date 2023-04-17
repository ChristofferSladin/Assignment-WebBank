using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;

namespace Assignment_WebBank.Services
{
    public interface ITransactionService
    {
        public enum ErrorCode
        {
            OK,
            BalanceTooLow,
            IncorrectAmount,
            AccountNotFound
        }

        List<TransactionsModel> GetTransactions(int accountId);
        ErrorCode Withdraw(int accountId, decimal amount);
        ErrorCode Deposit(int accountId, decimal amount);
        ErrorCode Transfer(int accountId, int toAccountId, decimal amount);
        List<AccountModel> GetAccounts(int accountId);
        AccountModel GetOneAccount(int accountId);
    }
}
