using BankLibrary.Services;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews
{
    public class UserUpdateViewModel : PageModel
    {
        private readonly ICRUDservice _crudService;
        public UserUpdateViewModel(ICRUDservice crudService)
        {
            _crudService = crudService;
        }

        public List<UserVM> UserList { get; set; }

        public void OnGet()
        {

            ViewData["HeaderText"] = "Update";
            ViewData["ButtonText"] = "Update";
            ViewData["RedirectUrl"] = "/Sections/CRUDviews/User/UpdateView";
            ViewData["Action"] = "Update";

            UserList = _crudService.GetAllUsers();
        }
    }
}
