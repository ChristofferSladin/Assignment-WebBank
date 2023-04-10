using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels.AccountViews
{
    public class AccountViewModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;

        public AccountViewModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }
        public ITransactionService.ErrorCode ErrorCodeEnums;
        public AccountModel OneAccount { get; set; }
        public List<AccountModel> Accounts { get; set; }
        public List<CustomerModel> Customers { get; set; }
        public int AccountId { get; set; }
        public void OnGet(int accountId, decimal amount, string CustomerId)
        {
            AccountId = accountId;
            OneAccount = _transactionService.GetOneAccount(accountId);
            Accounts = _transactionService.GetAccounts(accountId);
            Customers = _customerService.GetOnlyCustomers();
            ErrorCodeEnums = _transactionService.Withdraw(accountId, amount);
            ErrorCodeEnums = _transactionService.Deposit(accountId, amount);
        }
    }
}
