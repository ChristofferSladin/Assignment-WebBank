using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class DenmarkViewModel : PageModel
    {
        private readonly IIndexService _indexService;

        public DenmarkViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<CustomerModel> DenmarkAccounts { get; set; }
        public void OnGet(string country)
        {
            DenmarkAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
        }
    }
}
