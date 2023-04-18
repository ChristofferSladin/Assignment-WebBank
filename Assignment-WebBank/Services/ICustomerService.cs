using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.Sections;

namespace Assignment_WebBank.Services
{
    public interface ICustomerService
    {
        List<CustomerVM> GetOnlyCustomers();
        PagedResult<CustomerVM> GetCustomers(string sortColumn, string sortOrder, string q, int CustomerId, int pageNo);
        CustomerVM GetCustomerCard(int customerId);
    }
}
