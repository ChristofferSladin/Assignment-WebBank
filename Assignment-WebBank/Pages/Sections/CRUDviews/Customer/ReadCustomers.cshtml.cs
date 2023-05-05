using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Pages.Sections.CRUDviews.Customer
{
    public class ReadCustomersModel : PageModel
    {
        private readonly ICustomerService _customerService;

        public ReadCustomersModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public PagedResult<CustomerVM>? Customers { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? Q { get; set; }
        public int CurrentPage { get; set; }
        public int CustomerId { get; set; }
        public int PageCount { get; set; }
        public int RowCount { get; set; }

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
            RowCount = Customers.RowCount;
        }
    }
}
