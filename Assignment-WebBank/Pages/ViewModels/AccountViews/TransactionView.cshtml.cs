using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_WebBank.Pages.ViewModels.AccountViews
{
    public class TransactionViewModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;

        public TransactionViewModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }
        
        public ITransactionService.ErrorCode ErrorCodeEnums;
        public List<TransactionsModel> Transactions { get; set; }
        public CustomerModel Customer { get; set; }
        public List<AccountModel> Accounts { get; set; }
        public int AccountId { get; set; }
        public void OnGet(int accountId, decimal amount, int customerId)
        {
            Customer = _customerService.GetCustomerCard(customerId);
            //Accounts = _transactionService.GetAccounts(accountId);
            Transactions = _transactionService.GetTransactions(accountId);
            ErrorCodeEnums = _transactionService.Withdraw(accountId, amount);
            ErrorCodeEnums = _transactionService.Deposit(accountId, amount);


        }
    }
}
