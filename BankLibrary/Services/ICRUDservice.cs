using BankLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public interface ICRUDservice
    {
        public List<UserVM> GetAllUsers();
        public UserVM GetOneUser(string id);
        public void DeleteUser(string id);
        void OnPostCreateUser(UserVM userVM);
    }
}
