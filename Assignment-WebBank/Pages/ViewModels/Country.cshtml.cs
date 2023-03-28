using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class CountryModel : PageModel
    {
        public int Id { get; set; }
        public int CustomerCount { get; set; }
        public int AccountCount { get; set; }
        public string Country { get; set; }
        public int TotalBalance { get; set; }
        public decimal Balance { get; set; }
       
        public void OnGet()
        {
        }
    }
}
