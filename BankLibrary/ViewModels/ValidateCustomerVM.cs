using Assignment_WebBank.Infrastructure.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.ViewModels
{
    public class ValidateCustomerVM
    {
        public int CustomerId { get; set; }

        [Required]
        public string? PersonalNr { get; set; }
        public int AccountNr { get; set; }
        public decimal Balance { get; set; }

        [Required]
        public string? Country { get; set; }



        [Required]
        [MinLength(2, ErrorMessage = "Length must be atlest 2 characters long")]
        [MaxLength(30, ErrorMessage = "Length must be atmost 30 characters long")]
        public string? FirstName { get; set; }


        [Required]
        [MinLength(2, ErrorMessage = "Length must be atlest 2 characters long")]
        [MaxLength(30, ErrorMessage = "Length must be atmost 30 characters long")]
        public string? LastName { get; set; }



        [Required]
        public string? Adress { get; set; }


        [Required]
        public string? City { get; set; }


        [Required(ErrorMessage = "Please select a gender.")]
        [NotEqualTo("0", ErrorMessage = "Please select a gender.")]
        public string? Gender { get; set; }


        [Required]
        public DateTime? BirthDay { get; set; }


        [Required]
        public string? PhoneNumber { get; set; }


        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
        ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }


        [Required]
        public string? ZipCode { get; set; }
    }
}
