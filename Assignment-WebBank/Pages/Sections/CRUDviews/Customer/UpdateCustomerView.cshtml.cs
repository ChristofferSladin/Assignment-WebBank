using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.Customer
{
    [Authorize(Roles = "Admin")]
    public class UpdateCustomerViewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
