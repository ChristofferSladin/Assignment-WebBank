using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.ViewModels
{
    public class UserVM
    {
        public string? UserId { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Length must be atlest 5 characters")]
        [MaxLength(50, ErrorMessage = "Length must be atmost 50 characters")]
        public string? UserName { get; set; }


        public string? NormalizedUserName { get; set; }


        [Required]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
           ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set;}


        public string? NormalizedEmail { get; set;}
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set;}
        public bool EmailConfirmed { get; set;}
        public int AccessFailedCount { get; set; }


    }
}
