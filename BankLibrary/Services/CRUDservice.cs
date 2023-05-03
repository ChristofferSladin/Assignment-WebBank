using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    [BindProperties]
    public class CRUDservice : ICRUDservice
    {
        private readonly BankAppDataContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        

        public CRUDservice(BankAppDataContext dbcontext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbcontext;
            _userManager = userManager;
        }

        public void CreateCustomer(CustomerVM newCustomer)
        {
            var customer = new Customer
            {
                NationalId = newCustomer.PersonalNr,
                Country = newCustomer.Country,
                Givenname = newCustomer.FirstName,
                Surname = newCustomer.LastName,
                Streetaddress = newCustomer.Adress,
                City = newCustomer.City,
                Gender = newCustomer.Gender,
                Birthday = newCustomer.BirthDay,
                Telephonenumber = newCustomer.PhoneNumber,
                Emailaddress = newCustomer.Email,
                CountryCode = "",
                Zipcode = newCustomer.ZipCode,
            };

            var newAccount = new Account
            {
                Frequency = "Monthly",
                Created = DateTime.Now,
                Balance = 0
            };

            var newDisposition = new Disposition
            {
                Account = newAccount,
                Customer = customer,
                Type = "OWNER"
            };

            _dbContext.Dispositions.Add(newDisposition);
            _dbContext.SaveChanges();
        }

        public List<UserVM> GetAllUsers() 
        {
            return _dbContext.AspNetUsers.Select(u => new UserVM
            {
                UserId = u.Id,
                UserName = u.UserName,
                NormalizedUserName = u.NormalizedUserName,
                Email = u.Email,
                NormalizedEmail = u.NormalizedEmail,
                PhoneNumber = u.PhoneNumber,
                PhoneNumberConfirmed = u.PhoneNumberConfirmed,
                AccessFailedCount = u.AccessFailedCount,
                EmailConfirmed = u.EmailConfirmed,

            }).ToList();
        }

        public UserVM GetOneUser(string id)
        {
            return new UserVM();
        }

        public void DeleteUser(string id)
        {
            var user = _dbContext.AspNetUsers.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dbContext.AspNetUsers.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        public async Task<IdentityResult> UpdateUser(string userId, string newUserName, string newEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "User not Found!" });
            }
            user.UserName = newUserName;
            user.Email = newEmail;

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task<IdentityResult> CreateUserAsync(UserVM userVM, string passsword, string roleName)
        {
            var user = new IdentityUser
            {
                UserName = userVM.UserName,
                Email = userVM.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, passsword);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }

            return result;
        }
    }
}
