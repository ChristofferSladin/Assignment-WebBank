using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class TransactionViewModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public TransactionViewModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        
        public ITransactionService.ErrorCode ErrorCodeEnums;
        public List<TransactionsModel> Transactions;
        public void OnGet(int accountId, decimal amount)
        {
            Transactions = _transactionService.GetAccount(accountId);
            ErrorCodeEnums = _transactionService.Withdraw(accountId, amount);
            ErrorCodeEnums = _transactionService.Deposit(accountId, amount);
            


        }
    }
}
