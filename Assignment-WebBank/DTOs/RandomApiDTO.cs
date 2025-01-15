using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.DTOs
{
    public class RandomApiDTO
    {
        public class User
        {
            public Name name { get; set; }
            public Location location { get; set; }
            public Dob dob { get; set; }
            public Picture picture { get; set; }
        }

        public class Name
        {
            public string title { get; set; }
            public string first { get; set; }
            public string last { get; set; }
        }

        public class Location
        {
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
        }

        public class Dob
        {
            public DateTime date { get; set; }
            public int age { get; set; }
        }

        public class Picture
        {
            public string large { get; set; }
            public string medium { get; set; }
            public string thumbnail { get; set; }
        }

        public class UserResult
        {
            public List<User> results { get; set; }
        }
    }
}
