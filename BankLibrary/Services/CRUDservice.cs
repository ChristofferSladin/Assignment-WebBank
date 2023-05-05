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
using AutoMapper;

namespace BankLibrary.Services
{
    [BindProperties]
    public class CRUDservice : ICRUDservice
    {
        private readonly BankAppDataContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        

        public CRUDservice(BankAppDataContext dbcontext, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _dbContext = dbcontext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> UpdateCustomerAsync(int customerId, ValidateCustomerVM CustomerToUpdate)
        {
            try
            {
                var customer = await _dbContext.Customers.FindAsync(customerId);

                if(customer == null)
                {
                    return IdentityResult.Failed(new IdentityError { Description = "Customer not Found" });
                }

                if (CustomerToUpdate.FirstName != null) customer.Givenname = CustomerToUpdate.FirstName;
                if (CustomerToUpdate.LastName != null) customer.Surname = CustomerToUpdate.LastName;
                if (CustomerToUpdate.PersonalNr != null) customer.NationalId = CustomerToUpdate.PersonalNr;
                if (CustomerToUpdate.BirthDay != DateTime.MinValue) customer.Birthday = CustomerToUpdate.BirthDay;
                if (CustomerToUpdate.Adress != null) customer.Streetaddress = CustomerToUpdate.Adress;
                if (CustomerToUpdate.City != null) customer.City = CustomerToUpdate.City;
                if (CustomerToUpdate.ZipCode != null) customer.Zipcode = CustomerToUpdate.ZipCode;
                if (CustomerToUpdate.Country != null) customer.Country = CustomerToUpdate.Country;
                if (CustomerToUpdate.PhoneNumber != null) customer.Telephonenumber = CustomerToUpdate.PhoneNumber;
                if (CustomerToUpdate.Email != null) customer.Emailaddress = CustomerToUpdate.Email;
                if (CustomerToUpdate.Gender != null) customer.Gender = CustomerToUpdate.Gender;


                await _dbContext.SaveChangesAsync();

                return IdentityResult.Success;

            }

            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = $"Error Updating Customer: {ex.Message}" });
            }
        }

        public ValidateCustomerVM GetOneCustomer(int customerId)
        {
            var customer = _dbContext.Customers.Find(customerId);

            if (customer == null)
            {
                return null;
            }

            var customerVM = _mapper.Map<ValidateCustomerVM>(customer);

            return customerVM;
        }

        public void CreateCustomer(ValidateCustomerVM newCustomer)
        {
            var customer = new Customer();

            customer.CountryCode = "";

            _mapper.Map(newCustomer, customer);

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
