using Assignment_WebBank.Model;
using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.Customer
{
    [Authorize(Roles = "Admin")]
    [BindProperties]
    public class CreateCustomerViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;

        public CreateCustomerViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }

        public CustomerVM NewCustomer { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                _crudService.CreateCustomer(NewCustomer);

                return RedirectToPage("/Sections/CRUDviews/Customer/CreateSuccessView");
            }
            else
            {
                return Page();
            }
        }
    }
}
