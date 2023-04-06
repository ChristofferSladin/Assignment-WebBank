using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;

namespace Assignment_WebBank.Services
{
    public interface ICustomerService
    {
        PagedResult<CustomerModel> GetCustomers(string sortColumn, string sortOrder, string q, int CustomerId, int pageNo);
        CustomerModel GetCustomerCard(int customerId);
    }
}
