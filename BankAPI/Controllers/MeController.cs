using Assignment_WebBank.BankAppData;
using Assignment_WebBank.Model;
using Assignment_WebBank.Services;
using AutoMapper;
using BankLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeController : ControllerBase
    {
        private readonly BankAppDataContext _dbContext;

        public MeController(BankAppDataContext dbContext)
        {
            _dbContext = dbContext;

        }

        // READ ONE CUSTOMER ///////////////////////////////////////////////////////
        /// <summary>
        /// Retrieve ONE Customer from the database
        /// </summary>
        /// <returns>
        /// One Customer
        /// </returns>
        /// <remarks>
        /// Example end point: GET /api/Account{id}
        /// </remarks>
        /// <response code="200">
        /// Successfully returned ONE Customer
        /// </response>

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CustomerDTO>> GetOne(int id)
        {
            var customer = _dbContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return BadRequest("Customer not found");
            }

            var customerModel = new CustomerDTO
            {
                CustomerId = customer.Result.CustomerId,
                FirstName = customer.Result.Givenname,
                LastName = customer.Result.Surname,
                PersonalNr = customer.Result.NationalId,
                Country = customer.Result.Country,
                City = customer.Result.City,
                Adress = customer.Result.Streetaddress,
                Gender = customer.Result.Gender,
                PhoneNumber = customer.Result.Gender,
                Email = customer.Result.Emailaddress,
                BirthDay = customer.Result.Birthday,
                ZipCode = customer.Result.Zipcode,
            };

            return Ok(customerModel);
        }


    }
}
