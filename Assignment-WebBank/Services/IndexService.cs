using Assignment_WebBank.Areas.Identity.Pages.Account.Manage;
using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Assignment_WebBank.Services
{
    public class IndexService : IIndexService
    {
        private readonly BankAppDataContext _dbContext;

        public IndexService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<IndexModelProps> GetCustomerAccountsByCountry(string country)
        {
            var customerAccounts = _dbContext.Customers
                .Join(_dbContext.Accounts, c => c.CustomerId, a => a.AccountId, (c, a) => new { Customer = c, Account = a })
                .Where(ca => ca.Customer.Country == country)
                .OrderByDescending(ca => ca.Account.Balance)
                .Take(10)
                .Select(ca => new IndexModelProps
                {
                    Id = ca.Customer.CustomerId,
                    Country = ca.Customer.Country,
                    Balance = ca.Account.Balance
                })
                .ToList();

            return customerAccounts;
        }
        //------------------------------------------------------------------
        //public List<IndexModelProps> CountryTotBalanceTotAccount()
        //{
        //    _dbContext.Customers.

        //    return 
        //}
        //----------------------------------------------------------------------
        //public void OnGet()
        //{
        //    var norwayAccounts = GetCustomerAccountsByCountry("Norway");
        //    var finlandAccounts = GetCustomerAccountsByCountry("Finland");
        //    var denmarkAccounts = GetCustomerAccountsByCountry("Denmark");
        //    var swedenAccounts = GetCustomerAccountsByCountry("Sweden");

        //    Countries.AddRange(norwayAccounts);
        //    Countries.AddRange(finlandAccounts);
        //    Countries.AddRange(denmarkAccounts);
        //    Countries.AddRange(swedenAccounts);
        //}
    }
}
