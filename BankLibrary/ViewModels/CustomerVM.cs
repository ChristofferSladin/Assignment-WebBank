using Assignment_WebBank.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;

namespace Assignment_WebBank.Model
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }

        [Required]
        public string? PersonalNr { get; set; }
        public int AccountNr { get; set; }
        public decimal Balance { get; set; }
        [Required]
        public string? Country { get; set; }



        [Required]
        [MinLength(5, ErrorMessage = "Amount must be atlest 5 characters long")]
        [MaxLength(30, ErrorMessage = "Amount must be atmost 30 characters long")]
        public string? FirstName { get; set; }


        [Required]
        [MinLength(5, ErrorMessage = "Amount must be atlest 5 characters long")]
        [MaxLength(30, ErrorMessage = "Amount must be atmost 30 characters long")]
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
        public string? ZipCode { get; set;}
    }
}
