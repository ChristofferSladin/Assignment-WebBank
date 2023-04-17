using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static Assignment_WebBank.Services.ITransactionService;

namespace Assignment_WebBank.Pages.ViewModels.AccountViews
{
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

        public int ToAccountId { get; set; }


        public List<TransactionsModel> Transactions { get; set; }
        public CustomerModel OneCustomer { get; set; }
        public List<AccountModel> Accounts { get; set; }
        public AccountModel OneAccount { get; set; }

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
