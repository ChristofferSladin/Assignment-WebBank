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

        List<TransactionsModel> GetAccount(int accountId);
        ErrorCode Withdraw(int accountId, decimal amount);
        ErrorCode Deposit(int accountId, decimal amount);

    }
}
