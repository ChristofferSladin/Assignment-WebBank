using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Assignment_WebBank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<CustomerModel> GetCustomers(string sortColumn, string sortOrder, string q, int CustomerId, int pageNo)
        {
            if (pageNo == 0) {pageNo = 1;}
                
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

            var firstItemIndex = (pageNo - 1) * 5; // 5 är page storlek

            query = query.Skip(firstItemIndex);
            query = query.Take(5); // 5 är page storlek

            return query.Select(c => new CustomerModel
            {
                CustomerId = c.CustomerId,
                Name = c.Givenname,
                Country = c.Country,
                City = c.City,
            }).ToList();
        }
    }
}
