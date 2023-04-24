using Assignment_WebBank.BankAppData;
using BankLibrary.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

        public CRUDservice(BankAppDataContext dbcontext)
        {
            _dbContext = dbcontext;
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

        public void OnPostCreateUser(UserVM user)
        {
                var newUser = new AspNetUser
                {
                    Id = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber= user.PhoneNumber,
                };

                // Add user to database
                _dbContext.AspNetUsers.Add(newUser);
                _dbContext.SaveChanges();
        }

    }
}
