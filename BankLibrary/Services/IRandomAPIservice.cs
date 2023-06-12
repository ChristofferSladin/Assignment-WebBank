using BankLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Services
{
    public interface IRandomAPIservice
    {
        Task<List<RandomApiDTO.User>> GetUsersAsync();
    }
}
