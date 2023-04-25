using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CustomerViews
{
    public class DenmarkViewModel : PageModel
    {
        private readonly IIndexService _indexService;

        public DenmarkViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<CustomerVM> DenmarkAccounts { get; set; }
        public void OnGet(string country)
        {
            DenmarkAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
        }
    }
}
