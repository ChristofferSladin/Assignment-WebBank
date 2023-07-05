using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.GeneralViews
{
    [ResponseCache(Duration = 60)]
    public class NorwayViewModel : PageModel
    {

        private readonly IIndexService _indexService;

        public NorwayViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<CustomerVM> NorwayAccounts { get; set; }
        public void OnGet(string country)
        {
            NorwayAccounts = _indexService.GetTopTenCustomerAccountsByCountry(country);
        }

    }
}
