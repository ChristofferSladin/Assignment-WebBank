using Assignment_WebBank.Model;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public interface ICRUDservice
    {
        List<UserVM> GetAllUsers();
        UserVM GetOneUser(string id);
        void DeleteUser(string id);
        Task<IdentityResult> CreateUserAsync(UserVM userVM, string password, string roleName);

        Task<IdentityResult> UpdateUser(string userId, string newUSerName, string newEmail);
        void CreateCustomer(ValidateCustomerVM newCustomer);
        ValidateCustomerVM GetOneCustomer(int customerId);

        Task<IdentityResult> UpdateCustomerAsync(int customerId, ValidateCustomerVM newCustomer);

    }
}
