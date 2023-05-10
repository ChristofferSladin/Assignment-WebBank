using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.User
{
    public class UserCredentials
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() // Full read/write access
            {
                UserName = "Customer",
                Password = "passwordCustomer",
            },
        };
    }

}
