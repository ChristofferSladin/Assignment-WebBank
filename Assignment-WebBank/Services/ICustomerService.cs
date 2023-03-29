using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;

namespace Assignment_WebBank.Services
{
    public interface ICustomerService
    {
        List<CustomerModel> GetCustomers(string sortColumn, string sortOrder);
    }
}
