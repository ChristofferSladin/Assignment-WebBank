using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.User
{
    [BindProperties(SupportsGet = true)]
    public class UpdateViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;

        public UpdateViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }
        
        public string UserId { get; set; }

        public UserVM UserVM { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        { 
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _crudService.UpdateUser(UserId, UserVM.UserName, UserVM.Email);

            if (result.Succeeded)
            {
                return RedirectToPage("/Sections/CRUDviews/User/UserUpdateView");
            }

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
