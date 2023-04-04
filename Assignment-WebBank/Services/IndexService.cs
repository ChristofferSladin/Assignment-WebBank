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
            var customerAccounts = _dbContext.Customers
                .Join(_dbContext.Accounts, c => c.CustomerId, a => a.AccountId, (c, a) => new { Customer = c, Account = a })
                .Where(ca => ca.Customer.Country == country)
                .OrderByDescending(ca => ca.Account.Balance)
                .Take(10)
                .Select(ca => new CustomerModel
                {
                    CustomerId = ca.Customer.CustomerId,
                    Country = ca.Customer.Country,
                    Balance = ca.Account.Balance,
                    AccountNr = ca.Account.AccountId,
                    Name = (ca.Customer.Givenname + " " + ca.Customer.Surname),
                    Adress = ca.Customer.Streetaddress,
                    City = ca.Customer.City,
                    PersonalNr = ca.Customer.NationalId
                })
                .ToList();

            return customerAccounts;
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
                            AccountCount = group.Select(cda => cda.CustomerDisposition.Customer.CustomerId).Distinct().Count(),
                            CustomerCount = group.Select(cda => cda.Account.AccountId).Distinct().Count(),
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
            //var countryList = new List<string>() { "Sweden", "Norway", "Denmark", "Finland" };

            //var indexModelPropsList = new List<IndexModelProps>();

            //foreach (var country in countryList)
            //{
            //    var customers = _dbContext.Customers.Where(c => c.Country == country).ToList();
            //    var customerIds = customers.Select(c => c.CustomerId).ToList();

            //    var dispositions = _dbContext.Dispositions.Where(d => customerIds.Contains(d.CustomerId)).ToList();

            //    var accounts = _dbContext.Accounts.Where(a => dispositions.Any(d => d.AccountId == a.AccountId)).ToList();

            //    var indexModelProps = new IndexModelProps
            //    {
            //        Country = country,
            //        TotalBalance = accounts.Sum(a => a.Balance),
            //        AccountCount = accounts.Count(),
            //        CustomerCount = customers.Count()
            //    };

            //    indexModelPropsList.Add(indexModelProps);
            //}

            //return indexModelPropsList;

    }
}
