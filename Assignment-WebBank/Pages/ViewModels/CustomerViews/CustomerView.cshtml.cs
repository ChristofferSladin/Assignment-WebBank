using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace Assignment_WebBank.Pages.ViewModels.CustomerViews
{
    [Authorize(Roles = "Admin, Cashier")]

    public class CustomerViewModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public CustomerViewModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public PagedResult<CustomerModel>? Customers { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? Q { get; set; }
        public int CurrentPage { get; set; }
        public int CustomerId { get; set; }
        public int PageCount { get; set; }

        public void OnGet(string sortColumn, string sortOrder, string q, int customerId, int pageNo)
        {
            if (pageNo == 0) { pageNo = 1; }

            Customers = _customerService.GetCustomers(sortColumn, sortOrder, q, CustomerId, pageNo);

            SortColumn = sortColumn;
            SortOrder = sortOrder;
            Q = q;
            CurrentPage = pageNo;
            CustomerId = customerId;
            PageCount = Customers.PageCount;
        }
    }
}
