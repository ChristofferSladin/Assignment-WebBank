using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.AccountViews
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
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
        public AccountVM OneAccount { get; set; }
        public List<AccountVM> Accounts { get; set; }
        public CustomerVM OneCustomer { get; set; }
        public List<CustomerVM> Customers { get; set; }
        public int AccountId { get; set; }
        public void OnGet(int accountId, decimal amount, int customerId)
        {
            AccountId = accountId;
            OneAccount = _transactionService.GetOneAccount(accountId);
            Accounts = _transactionService.GetAccounts(accountId);
            OneCustomer = _customerService.GetCustomerCard(customerId);
            Customers = _customerService.GetOnlyCustomers();
            ErrorCodeEnums = _transactionService.Withdraw(accountId, amount);
            ErrorCodeEnums = _transactionService.Deposit(accountId, amount);
        }
    }
}
