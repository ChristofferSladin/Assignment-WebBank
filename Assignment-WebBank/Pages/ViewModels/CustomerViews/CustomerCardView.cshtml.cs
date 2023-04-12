using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_WebBank.Pages.ViewModels.CustomerViews
{

    public class CustomerCardViewModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerCardViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public CustomerModel OneCustomerCard { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public void OnGet(int customerId, int accountId)
        {
            AccountId = accountId;
            CustomerId = customerId;
            OneCustomerCard = _customerService.GetCustomerCard(customerId);
        }
    }
}