using Assignment_WebBank.Areas.Identity.Pages.Account.Manage;
using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq;

namespace Assignment_WebBank.Services
{
    public class IndexService : IIndexService
    {
        private readonly BankAppDataContext _dbContext;

        public IndexService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CustomerModel> GetTopTenCustomerAccountsByCountry(string country)
        {
            var top10CustomerInCountryList = _dbContext.Customers
                .Where(customer => customer.Country == country)
                .Select(c => new
                {
                    Customer = c,
                    TotalBalance = _dbContext.Dispositions
                    .Where(d => d.CustomerId == c.CustomerId)
                    .Join(_dbContext.Accounts, d => d.AccountId, a => a.AccountId, (d, a) => a.Balance)
                    .Sum()
                })
                .OrderByDescending(c => c.TotalBalance)
                .Take(10)
                .Select(c => new CustomerModel
                {
                    CustomerId = c.Customer.CustomerId,
                    FirstName = c.Customer.Givenname,
                    LastName = c.Customer.Surname,
                    Balance = c.TotalBalance,
                    Country = c.Customer.Country,
                    Adress = c.Customer.Streetaddress,
                    City = c.Customer.City,
                    PersonalNr = c.Customer.NationalId

                }).ToList();

            return top10CustomerInCountryList;
        }

        public List<IndexModelProps> CountryTotBalanceAndTotAccount()
        {
            var listOfCountries = new List<string>() { "Sweden", "Norway", "Denmark", "Finland" };
            var countriesList = new List<IndexModelProps>();

            foreach (var country in listOfCountries)
            {
                var countries = _dbContext.Customers
                    .Where(c => c.Country == country)
                    .Join(_dbContext.Dispositions, c => c.CustomerId, d => d.CustomerId, (c, d) => new { Customer = c, Disposition = d })
                    .Join(_dbContext.Accounts, cd => cd.Disposition.AccountId, a => a.AccountId, (cd, a) => new { CustomerDisposition = cd, Account = a })
                    .GroupBy(cda => cda.CustomerDisposition.Customer.Country)
                    .Select(group => new IndexModelProps
                    {
                        Country = group.Key,
                        CustomerCount = group.Select(cda => cda.CustomerDisposition.Customer.CustomerId).Distinct().Count(),
                        AccountCount = group.Select(cda => cda.Account.AccountId).Distinct().Count(),
                        TotalBalance = group.Sum(cda => cda.Account.Balance),
                    })
                    .FirstOrDefault();

                if (countries != null)
                {
                    countriesList.Add(countries);
                }
            }
            return countriesList;
        }
    }
}
