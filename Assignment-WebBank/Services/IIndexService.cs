using Assignment_WebBank.Areas.Identity.Pages.Account.Manage;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;

namespace Assignment_WebBank.Services
{
    public interface IIndexService
    {
        List<IndexModelProps> GetCustomerAccountsByCountry(string country);

        List<IndexModelProps> CountryTotBalanceAndTotAccount();
    }
}
