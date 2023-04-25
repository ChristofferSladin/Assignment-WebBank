using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Assignment_WebBank.Pages.Sections.CRUDviews
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class UserCreateViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;

        public UserCreateViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }
        
        public UserVM UserVM { get; set; }

        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Role { get; set; }
        [Required] public string Email { get; set; }
        

        public void OnGet()
        {
        }

        [HttpPost]
        public async Task<IActionResult> OnPost(string password)
{
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _crudService.CreateUserAsync(UserVM, password, Role);

            if (!result.Succeeded)
            {
                return Page();
            }

            return RedirectToPage("/Sections/CRUDviews/UserUpdateView");
        }
    }
}
