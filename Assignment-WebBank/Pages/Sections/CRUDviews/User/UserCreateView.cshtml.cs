using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CRUDviews
{
    [Authorize(Roles = "Admin")]
    public class UserCreateViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;

        public UserCreateViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }

        [BindProperty]
        public UserVM UserVM { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPostCreateUser()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _crudService.OnPostCreateUser(UserVM);

            return RedirectToPage("/Sections/CRUDviews/User/UserCreateView");
        }
    }
}
