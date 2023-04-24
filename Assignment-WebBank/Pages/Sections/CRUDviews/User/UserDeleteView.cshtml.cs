using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews
{
    public class UserDeleteViewModel : PageModel
    {


        private readonly ICRUDservice _crudService;
        public UserDeleteViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }

        public List<UserVM> UserList { get; set; }

        public void OnGet()
        {

            ViewData["HeaderText"] = "DELETE";
            ViewData["ButtonText"] = "Delete";
            ViewData["RedirectUrl"] = "/Sections/CRUDviews/User/DeleteView";

            UserList = _crudService.GetAllUsers();
        }

        public IActionResult OnGetDelete(string userId)
        {
            _crudService.DeleteUser(userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
