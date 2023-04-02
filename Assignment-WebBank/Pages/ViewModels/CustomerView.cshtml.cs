using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.ViewModels
{
    public class CustomerViewModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public List<CustomerModel>? Customers { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? Q { get; set; }
        public int CurrentPage { get; set; }
        public int CustomerId { get; set; }

        public void OnGet(string sortColumn, string sortOrder, string q, int CustomerId, int pageNo)
        {
            if (pageNo == 0) { pageNo = 1; }

            SortColumn = sortColumn;
            SortOrder = sortOrder;
            Q = q;
            CurrentPage = pageNo;
            this.CustomerId = CustomerId;

            Customers = _customerService.GetCustomers(sortColumn, sortOrder, q, CustomerId, pageNo);  
        }
    }
}
