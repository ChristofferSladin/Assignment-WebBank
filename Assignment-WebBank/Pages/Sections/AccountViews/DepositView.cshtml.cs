using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static Assignment_WebBank.Services.ITransactionService;

namespace Assignment_WebBank.Pages.Sections.AccountViews
{
    [BindProperties]
    public class DepositViewModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;

        public DepositViewModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }


        [Required]
        [Range(100, 10000, ErrorMessage = "Amount must be atlest 100 and atmost 10000")]
        public decimal Amount { get; set; }

        public DateTime DepositDate { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Must be atleast 5 characters and atmost 250 characters")]
        [MaxLength(250, ErrorMessage = "Must be atleast 5 characters and atmost 250 characters")]
        public string? Comment { get; set; }


        public List<TransactionVM> Transactions { get; set; }
        public CustomerVM OneCustomer { get; set; }
        public List<AccountVM> Accounts { get; set; }
        public AccountVM OneAccount { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            OneCustomer = _customerService.GetCustomerCard(customerId);
            Accounts = _transactionService.GetAccounts(accountId);
            Transactions = _transactionService.GetTransactions(accountId);
            OneAccount = _transactionService.GetOneAccount(accountId);
            DepositDate = DateTime.Now;
        }

        public IActionResult OnPost(int accountId, int customerId)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionService.Deposit(accountId, Amount);

                if (status == ErrorCode.OK)
                {
                    ViewData["Message"] = "Successfully deposited money!";
                }

                if (status == ErrorCode.IncorrectAmount)
                {
                    ModelState.AddModelError("Amount", "Please enter a correct amount (100-10000)!");
                }

                if (DepositDate.AddHours(1) < DateTime.Now)
                {
                    ModelState.AddModelError("DepositDate", "Cannot Deposit money in the past!");
                }
            }

            return Page();
        }
    }
}
