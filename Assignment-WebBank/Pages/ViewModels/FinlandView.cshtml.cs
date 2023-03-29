using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class FinlandViewModel : PageModel
    {
        private readonly IIndexService _indexService;

        public FinlandViewModel(IIndexService indexService)
        {
            _indexService = indexService;
        }
        public List<IndexModelProps> FinlandAccounts { get; set; }
        public void OnGet(string country)
        {
            FinlandAccounts = _indexService.GetCustomerAccountsByCountry(country);
        }
    }
}
