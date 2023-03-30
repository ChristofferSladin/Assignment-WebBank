using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class NorwayViewModel : PageModel
    {

        private readonly IIndexService _indexService;

        public NorwayViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<CustomerModel> NorwayAccounts { get; set; }
        public void OnGet(string country)
        {
            NorwayAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
        }

    }
}
