using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class CountriesViewModel : PageModel
    {

        public int Id { get; set; }
        public string Country { get; set; }
        public decimal Balance { get; set; }
        public void OnGet()
        {
        }
    }
}
