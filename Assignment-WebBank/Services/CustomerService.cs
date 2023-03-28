using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Pages.ViewModels;

namespace Assignment_WebBank.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankAppDataContext _dbContext;

        public CustomerService(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Customer> GetCustomers(string sortColumn, string sortOrder)
        {
            var query = _dbContext.Customers.AsQueryable();

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

            return query.ToList();
        }
    }
}
