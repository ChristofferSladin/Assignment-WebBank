using Assignment_WebBank.Areas.Identity.Pages.Account.Manage;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.Sections;

namespace Assignment_WebBank.Services
{
    public interface IIndexService
    {
        List<CustomerVM> GetTopTenCustomerAccountsByCountry(string country);

        List<IndexVM> CountryTotBalanceAndTotAccount();
    }
}
