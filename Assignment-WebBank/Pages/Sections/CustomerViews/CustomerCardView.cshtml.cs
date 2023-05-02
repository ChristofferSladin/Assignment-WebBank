using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CustomerViews
{
    [Authorize(Roles = "Cashier")]
    public class CustomerCardViewModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerCardViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public CustomerVM OneCustomerCard { get; set; }
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
