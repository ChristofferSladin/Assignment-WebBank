using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexService _indexService;
        private readonly ICustomerService _customerService;

        public IndexModel(IIndexService indexService, ICustomerService customerService)
        {
            _indexService = indexService;
            _customerService = customerService;
        }
        public List<CustomerVM>? CountriesAccounts { get; set; }

        public List<IndexVM> TotBalanceTotAccountCountry { get; set; }

        public void OnGet(string country)
        {
            CountriesAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
            TotBalanceTotAccountCountry = _indexService.CountryTotBalanceAndTotAccount();
        }
    }
}