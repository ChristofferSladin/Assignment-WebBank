using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MvvmHelpers;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class CustomerViewModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public List<CustomerModel> Customers { get; set; }

        public void OnGet(string sortColumn, string sortOrder)
        {
            Customers = _customerService.GetCustomers(sortColumn, sortOrder)
                .Select(s => new CustomerModel
                {
                    Id = s.CustomerId,
                    Name = s.Givenname,
                    City = s.City,
                    Country = s.Country
                }).ToList();
        }
    }
}
