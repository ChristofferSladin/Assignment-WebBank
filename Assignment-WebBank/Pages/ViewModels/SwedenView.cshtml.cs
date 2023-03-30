using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class SwedenViewModel : PageModel
    {
        private readonly IIndexService _indexService;

        public SwedenViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<CustomerModel> SwedenAccounts { get; set; }
        public void OnGet(string country)
        {
            SwedenAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
        }
    }
}