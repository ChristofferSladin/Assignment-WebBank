using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CRUDviews
{
    [Authorize(Roles = "Admin")]
    public class CrudStartViewModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
