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
            var customer = _dbContext.Customers
                .Where(c => c.CustomerId == customerId)
                .Join(_dbContext.Dispositions, c => c.CustomerId, d => d.CustomerId, (c, d) => new { Customer = c, Disposition = d })
                .Join(_dbContext.Accounts, cd => cd.Disposition.AccountId, a => a.AccountId, (cd, a) => new { CustomerDisposition = cd, Account = a })
                .GroupBy(cda => cda.CustomerDisposition.Customer.Country)
                .Select(group => new CustomerModel
                {
                    Country = group.Key,
                    CustomerId = group.Select(cda => cda.CustomerDisposition.Customer.CustomerId).First(),
                    AccountNr = group.Select(cda => cda.Account.AccountId).First(),
                    Balance = group.Select(cda => cda.Account.Balance).First(),
                    FirstName = group.Select(cda => cda.CustomerDisposition.Customer.Givenname).First(),
                    LastName = group.Select(cda => cda.CustomerDisposition.Customer.Surname).First(),
                    Adress = group.Select(cda => cda.CustomerDisposition.Customer.Streetaddress).First(),
                    City = group.Select(cda => cda.CustomerDisposition.Customer.City).First(),
                    PersonalNr = group.Select(cda => cda.CustomerDisposition.Customer.NationalId).First(),
                    Gender = group.Select(cda => cda.CustomerDisposition.Customer.Gender).First(),
                    BirthDay = group.Select(cda => cda.CustomerDisposition.Customer.Birthday).First(),
                    PhoneNumber = group.Select(cda => cda.CustomerDisposition.Customer.Telephonenumber).First(),
                    Email = group.Select(cda => cda.CustomerDisposition.Customer.Emailaddress).First()
                })
                .FirstOrDefault();

            return customer;
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

            var result = query.GetPaged(pageNo, 50);

            var customerModelList = result.Results.Select(p => new CustomerModel
           {
               CustomerId = p.CustomerId,
               PersonalNr = p.NationalId,
               FirstName = p.Givenname,
               LastName = p.Surname,
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
