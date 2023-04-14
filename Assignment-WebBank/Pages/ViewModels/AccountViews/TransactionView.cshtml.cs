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

        public int AccountId { get; set; }
        public decimal Amount { get; set; }

        public int TransactionsPerPage { get; set; }

        public void OnGet(int accountId, decimal amount, int customerId, int transactionsPerPage)
        {
            Amount = amount;
            AccountId = accountId;
            Customer = _customerService.GetCustomerCard(customerId);
            Transactions = _transactionService.GetTransactions(accountId);

            if (transactionsPerPage > 0)
            {
                TransactionsPerPage = transactionsPerPage;
            }
            else
            {
                TransactionsPerPage = int.MaxValue;
            }
        }
    }
}
