using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using BankLibrary.DTOs;
using BankLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages
{
    [ResponseCache(Duration = 300)]
    public class IndexModel : PageModel
    {
        private readonly IIndexService _indexService;
        private readonly ICustomerService _customerService;
        private readonly IRandomAPIservice _randomAPIservice;

        public IndexModel(IIndexService indexService, ICustomerService customerService, IRandomAPIservice randomAPIservice)
        {
            _indexService = indexService;
            _customerService = customerService;
            _randomAPIservice = randomAPIservice;
        }
        public List<CustomerVM>? CountriesAccounts { get; set; }
        public List<RandomApiDTO.User> Users { get; set; }
        public List<IndexVM> TotBalanceTotAccountCountry { get; set; }
        public List<string> Countries { get; set; } = new List<string> { "Sweden", "Norway", "Denmark", "Finland"};

        public async Task OnGetAsync(string country)
        {
            Users = await _randomAPIservice.GetUsersAsync();
            CountriesAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
            TotBalanceTotAccountCountry = _indexService.CountryTotBalanceAndTotAccount();
        }
    }
}