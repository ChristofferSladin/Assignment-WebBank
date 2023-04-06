using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Infrastructure.Paging;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Metrics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Assignment_WebBank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CustomerModel GetCustomerCard(int customerId)
        {
            var customerAccount = _dbContext.Customers
            .Join(_dbContext.Accounts, c => c.CustomerId, a => a.AccountId, (c, a) => new { Customer = c, Account = a })
            .Where(ca => ca.Customer.CustomerId == customerId)
            .First();

            if (customerAccount == null)
            {
                return null;
            }

            var customerModel = new CustomerModel
            {
                CustomerId = customerAccount.Customer.CustomerId,
                Country = customerAccount.Customer.Country,
                Balance = customerAccount.Account.Balance,
                AccountNr = customerAccount.Account.AccountId,
                Name = $"{customerAccount.Customer.Givenname} {customerAccount.Customer.Surname}",
                Adress = customerAccount.Customer.Streetaddress,
                City = customerAccount.Customer.City,
                PersonalNr = customerAccount.Customer.NationalId,
                Gender = customerAccount.Customer.Gender,
                BirthDay = customerAccount.Customer.Birthday?.ToString("dd-MM-yyyy"),
                PhoneNumber = customerAccount.Customer.Telephonenumber,
                Email = customerAccount.Customer.Emailaddress

            };
            return customerModel;
        }

        public PagedResult<CustomerModel> GetCustomers(string sortColumn, string sortOrder, string q, int CustomerId, int pageNo)
        {
            if (string.IsNullOrEmpty(sortOrder))
                sortOrder = "asc";
            if (string.IsNullOrEmpty(sortColumn))
                sortColumn = "Id";

            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(p => p.Givenname.Contains(q) || p.Surname.Contains(q) || p.Country.Contains(q) || p.City.Contains(q));
            }

            if (sortColumn == "Id")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.CustomerId);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.CustomerId);

            if (sortColumn == "Name")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Givenname);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Givenname);

            if (sortColumn == "Country")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.Country);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.Country);

            if (sortColumn == "City")
                if (sortOrder == "asc")
                    query = query.OrderBy(s => s.City);
                else if (sortOrder == "desc")
                    query = query.OrderByDescending(s => s.City);

            //var firstItemIndex = (pageNo - 1) * 50; // 5 är page storlek

            //query = query.Skip(firstItemIndex);
            //query = query.Take(50); // 5 är page storlek

            var result = query.GetPaged(pageNo, 50);

            

            var customerModelList = result.Results.Select(p => new CustomerModel
           {
               CustomerId = p.CustomerId,
               PersonalNr = p.NationalId,
               Name = $"{p.Givenname} {p.Surname}",
               Adress = p.Streetaddress,
               City = p.City,
               Country = p.Country,
           }).ToList();

            return new PagedResult<CustomerModel>
            {
                CurrentPage = result.CurrentPage,
                PageCount = result.PageCount,
                PageSize = result.PageSize,
                RowCount = result.RowCount,
                Results = customerModelList
            };
        }
    }
}
