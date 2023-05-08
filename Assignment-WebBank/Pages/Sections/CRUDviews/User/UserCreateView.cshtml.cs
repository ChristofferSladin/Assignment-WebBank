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

        [Required]
        [MinLength(5, ErrorMessage = "Amount must be atlest 5 characters long")]
        [MaxLength(50, ErrorMessage = "Amount must be atmost 50 characters long")]
        public string UserName { get; set; }


        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=!]).{8,20}$",
            ErrorMessage = "Password must contain at least one uppercase letter," +
            " one lowercase letter, one digit, and one special character (@#$%^&+=!)," +
            " and be between 8 and 20 characters in length.")]
         public string Password { get; set; }

        
        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
            ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required] public string Role { get; set; }


        public void OnGet()
        {
        }

        [HttpPost]
        public async Task<IActionResult> OnPost(string password)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _crudService.CreateUserAsync(UserVM, password, Role);

            if (!result.Succeeded)
            {
                return Page();
            }

            ViewData["Message"] = "Successfully Updated User!";

            return RedirectToPage("/Sections/CRUDviews/User/UserCreateSuccess");
        }
    }
}
