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
        public List<UserVM> GetAllUsers();
        public UserVM GetOneUser(string id);
        public void DeleteUser(string id);
        Task<IdentityResult> CreateUserAsync(UserVM userVM, string password, string roleName);

        public Task<IdentityResult> UpdateUser(string userId, string newUSerName, string newEmail);
        void CreateCustomer(CreateCustomerVM newCustomer);
    }
}
