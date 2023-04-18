using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CustomerViews
{
    [Authorize(Roles = "Admin, Cashier")]
    public class FinlandViewModel : PageModel
    {
        private readonly IIndexService _indexService;

        public FinlandViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<CustomerVM> FinlandAccounts { get; set; }
        public void OnGet(string country)
        {
            FinlandAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
        }
    }
}
