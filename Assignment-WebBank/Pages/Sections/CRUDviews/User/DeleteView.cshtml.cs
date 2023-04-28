using BankLibrary.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.User
{
    public class DeleteViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;
        public DeleteViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }
        
        [BindProperty(SupportsGet = true)]
        public string UserId { get; set; }


        public void OnGet()
        {
        }
        public IActionResult OnPostAsync()
        {
            _crudService.DeleteUser(UserId);
            return RedirectToPage("/Sections/CRUDviews/User/UserDeleteView");
        }
    }
}
