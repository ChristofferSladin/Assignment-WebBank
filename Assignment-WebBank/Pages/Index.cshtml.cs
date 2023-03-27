using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Runtime.CompilerServices;

namespace Assignment_WebBank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BankAppDataContext _dbContext;

        public IndexModel(ILogger<IndexModel> logger, BankAppDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public List<CountriesViewModel> Countries { get; set; } = new List<CountriesViewModel>();

        public void OnGet()
        {
            var norwayAccounts = GetCustomerAccountsByCountry("Norway");
            var finlandAccounts = GetCustomerAccountsByCountry("Finland");
            var denmarkAccounts = GetCustomerAccountsByCountry("Denmark");
            var swedenAccounts = GetCustomerAccountsByCountry("Sweden");

            Countries.AddRange(norwayAccounts);
            Countries.AddRange(finlandAccounts);
            Countries.AddRange(denmarkAccounts);
            Countries.AddRange(swedenAccounts);
        }

        public List<CountriesViewModel> GetCustomerAccountsByCountry(string country)
        {
            var customerAccounts = _dbContext.Customers
                .Join(_dbContext.Accounts, c => c.CustomerId, a => a.AccountId, (c, a) => new { Customer = c, Account = a })
                .Where(ca => ca.Customer.Country == country)
                .OrderByDescending(ca => ca.Account.Balance)
                .Take(10)
                .Select(ca => new CountriesViewModel
                {
                    Id = ca.Customer.CustomerId,
                    Country = ca.Customer.Country,
                    Balance = ca.Account.Balance
                })
                .ToList();

            return customerAccounts;
        }
    }
}