using Assignment_WebBank.Data;
using Assignment_WebBank.Infrastructure.Validation;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using static Assignment_WebBank.Services.ITransactionService;

namespace Assignment_WebBank.Pages.Sections.AccountViews
{
    [Authorize(Roles = "Cashier")]
    [BindProperties]
    public class TransferViewModel : PageModel
    {
        private readonly ITransactionService _transactionService;
        private readonly ICustomerService _customerService;

        public TransferViewModel(ITransactionService transactionService, ICustomerService customerService)
        {
            _transactionService = transactionService;
            _customerService = customerService;
        }


        [Required]
        [Range(100, 10000, ErrorMessage = "Amount must be atlest 100 and atmost 10000")]
        public decimal Amount { get; set; }

        public DateTime TransferDate { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Must be atleast 5 characters and atmost 250 characters")]
        [MaxLength(250, ErrorMessage = "Must be atleast 5 characters and atmost 250 characters")]
        public string? Comment { get; set; }


        [Required]
        [MinValue(1)]
        public int ToAccountId { get; set; }


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
            TransferDate = DateTime.Now;
        }
        public IActionResult OnPost(int accountId, decimal amount)
        {
            if (ModelState.IsValid)
            {
                var status = _transactionService.Transfer(accountId, ToAccountId, amount);

                if (status == ErrorCode.OK)
                {
                    ViewData["Message"] = "Successfully Transfered money!";
                }

                if (status == ErrorCode.BalanceTooLow) { ModelState.AddModelError("Amount", "You don't have that much money!"); }

                if (status == ErrorCode.IncorrectAmount)
                {
                    ModelState.AddModelError("Amount", "Please enter a correct amount (100-10000)!");
                }
                

                if (TransferDate.AddHours(1) < DateTime.Now)
                {
                    ModelState.AddModelError("TransferDate", "Cannot Transfer money in the past!");
                }
            }

            return Page();
        }
    }
}
