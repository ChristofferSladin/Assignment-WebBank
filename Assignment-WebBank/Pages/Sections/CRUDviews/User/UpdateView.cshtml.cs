using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.User
{
    [Authorize(Roles = "Admin")]
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
                return RedirectToPage("/Sections/CRUDviews/User/UserUpdateSuccess");
            }

            return Page();
        }
    }
}
