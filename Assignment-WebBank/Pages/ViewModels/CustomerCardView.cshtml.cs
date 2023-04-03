using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class CustomerCardViewModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerCardViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public CustomerModel OneCustomerCard { get; set; }
        public void OnGet(int customerId)
        {
            OneCustomerCard = _customerService.GetCustomerCard(customerId);
        }
    }
}