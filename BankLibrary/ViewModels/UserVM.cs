using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.ViewModels
{
    public class UserVM
    {
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set;}
        public string? NormalizedEmail { get; set;}
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set;}
        public bool EmailConfirmed { get; set;}
        public int AccessFailedCount { get; set; }


    }
}
