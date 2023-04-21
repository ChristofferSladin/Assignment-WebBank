
using Assignment_WebBank.Model;


namespace Assignment_WebBank.Services
{
    public interface IIndexService
    {
        List<CustomerVM> GetTopTenCustomerAccountsByCountry(string country);

        List<IndexVM> CountryTotBalanceAndTotAccount();
    }
}
