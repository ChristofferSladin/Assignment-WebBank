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

        List<TransactionVM> GetTransactions(int accountId);
        ErrorCode Withdraw(int accountId, decimal amount);
        ErrorCode Deposit(int accountId, decimal amount);
        ErrorCode Transfer(int accountId, int toAccountId, decimal amount);
        List<AccountVM> GetAccounts(int accountId);
        AccountVM GetOneAccount(int accountId);
    }
}
