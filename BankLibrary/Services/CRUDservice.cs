using Assignment_WebBank.BankAppData;
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
