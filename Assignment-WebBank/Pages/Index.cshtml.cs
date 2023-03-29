﻿using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public List<IndexModelProps> TotBalanceTotAccountCountry { get; set; }

        public void OnGet(string country)
        {
            CountriesAccounts = _indexService.GetCustomerAccountsByCountry(country);
            TotBalanceTotAccountCountry = _indexService.CountryTotBalanceAndTotAccount();
        }
    }
}