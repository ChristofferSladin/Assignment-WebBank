using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace Assignment_WebBank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexService _indexService;

        public IndexModel(IIndexService indexService)
        {
            _indexService = indexService;
        }

        public List<IndexModelProps>? CountriesAccounts { get; set; }

        public void OnGet(string country)
        {
            CountriesAccounts = _indexService.GetCustomerAccountsByCountry(country);
            //var norwayAccounts = GetCustomerAccountsByCountry("Norway");
            //var finlandAccounts = GetCustomerAccountsByCountry("Finland");
            //var denmarkAccounts = GetCustomerAccountsByCountry("Denmark");
            //var swedenAccounts = GetCustomerAccountsByCountry("Sweden");

            //CountriesAccounts.AddRange(norwayAccounts);
            //CountriesAccounts.AddRange(finlandAccounts);
            //CountriesAccounts.AddRange(denmarkAccounts);
            //CountriesAccounts.AddRange(swedenAccounts);
        }
    }
}