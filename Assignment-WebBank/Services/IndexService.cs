using Assignment_WebBank.Areas.Identity.Pages.Account.Manage;
using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

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
        
        public List<IndexModelProps> CountryTotBalanceAndTotAccount()
        {
            var countryList = new List<string>() { "Sweden", "Norway", "Denmark", "Finland" };

            var indexModelPropsList = new List<IndexModelProps>();

            foreach (var country in countryList)
            {
                var indexModelProps = _dbContext.Customers
                    .Where(c => c.Country == country)
                    .Join(_dbContext.Accounts, c => c.CustomerId, a => a.AccountId, (c, a) => new { Customer = c, Account = a })
                    .GroupBy(ca => ca.Customer.Country)
                    .Select(group => new IndexModelProps
                    {
                        Country = group.Key,
                        TotalBalance = group.Sum(ca => ca.Account.Balance),
                        AccountCount = group.Count()
                    })
                    .FirstOrDefault();

                if (indexModelProps != null)
                {
                    indexModelPropsList.Add(indexModelProps);
                }
            }
            return indexModelPropsList;
        }
    }
}
