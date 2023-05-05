using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.Customer
{
    [Authorize(Roles = "Admin")]
    public class UpdateCustomerViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;

        public UpdateCustomerViewModel(ICRUDservice crudService)
        {
           _crudService = crudService;
        }

        [BindProperty] public ValidateCustomerVM Customer { get; set; }

        [BindProperty] public int CustomerId { get; set; }

        public void OnGet(int customerId)
        {
            CustomerId = customerId;
            Customer = _crudService.GetOneCustomer(customerId);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _crudService.UpdateCustomerAsync(CustomerId, Customer);

            if(result.Succeeded)
            {
                return RedirectToPage("/Sections/CRUDviews/Customer/UpdateSuccess");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();

        }
    }
}
